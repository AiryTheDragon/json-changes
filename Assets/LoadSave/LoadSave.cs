using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

public class LoadSave : MonoBehaviour, INeedsClockUpdate
{
    private NPCBehavior[] npcList;
    private LampBehavior[] lampList;
    private GameObject[] paperList;
    private GameObject[] penList;
    private GameObject[] keyList;
    private GameObject[] miscList; // TODO

    private readonly AchievementSettings achievementSettings = new();

    public InvScript invSettings; // TODO
    public BannedActivitiesBehavior bannedActivitiesSettings; // TODO
    public Log logSettings; // TODO
    public ClockBehavior Clock;
    public Player player;

    private SaveFile saveFile;

    private string fileFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
    private string subpathString = "EEData";
    private string pathString;


    public void Start()
    {
        pathString = System.IO.Path.Combine(fileFolder, subpathString);

        saveFile = new SaveFile();

        npcList = GameObject.FindObjectsOfType<NPCBehavior>();
        lampList = GameObject.FindObjectsOfType<LampBehavior>();
        paperList = GameObject.FindGameObjectsWithTag("Paper");
        penList = GameObject.FindGameObjectsWithTag("Pen");
        keyList = GameObject.FindGameObjectsWithTag("Key");
        player = GameObject.FindObjectOfType<Player>();

        if(string.IsNullOrWhiteSpace(Player.Name))
        {
            ImportSaveData($"save{Player.SaveFile}");
            LoadSaveData($"save{Player.SaveFile}");
        }

        Clock.NeedsClockUpdate.Add(this);
    }

    public void CreateSaveData(string saveName)
    {
        saveFile = new();
        SavePlayerName();
        SaveClockTime();
        CreateBehaviorSettings();
        CreatePositionSettings();
        CreateLightSettings();
        CreateObjectSettings();
        CreateAchievementSettings();
        CreateInventory();
        CreateLog();
    }

    public void LoadSaveData(string saveName)
    {
        saveFile = new();
        LoadClockTime();
        LoadPlayerName();
        SetBehaviorSettings();
        SetPositionSettings();
        SetLightSettings();
        SetObjectSettings();
        SetAchievementSettings();
        SetInventory();
        SetLog();
    }
    
    /// <summary>
    /// This method is to be used during development process to create
    /// a good next day state for the game.
    /// </summary>
    public void CreateNextDayState()
    {
        saveFile = new();
        CreateBehaviorSettings();
        CreatePositionSettings();
        CreateLightSettings();
    }

    /// <summary>
    /// This method loads the data regarding the next day state into memory
    /// from files, then sets the values regarding the behavior and positions
    /// of the NPCs and player, and also sets the light object settings.
    /// </summary>
    public void LoadNextDayState()
    {
        saveFile = new();
        ImportNextDayState();
        SetBehaviorSettings();
        SetPositionSettings();
        SetLightSettings();      
        //TODO update suspecion, based on time slept.
    }

    // Do I need this one?
    public void SaveNextDayState()
    {
        
    }

    /*--------------------------------------------------------
     * This method is to be used to save to the user's hard drive
     * The data to set up the next day
     * 
     * This method should only be used by the developers when
     * they need to adjust the world data for when the player 
     * wakes up  (such as NPC positions, lights on, etc.)
     * ------------------------------------------------------*/
    public void ExportNextDayState()
    {
        Debug.Log("Testing File saving");

        // save next day data

        string nextDayFileName = "NextDay.json";
        string totalPathString = System.IO.Path.Combine(pathString, nextDayFileName);
        string nextDayData = JsonConvert.SerializeObject(saveFile); // converts data in the NPCsettings object into a string

        System.IO.Directory.CreateDirectory(pathString);

        if (System.IO.File.Exists(totalPathString))
        {
            Debug.Log("Overwriting file \"" + totalPathString + "\".");
        }
        
        using (System.IO.FileStream fs = System.IO.File.Create(totalPathString))
        {
            using var sr = new StreamWriter(fs);

            sr.Write(nextDayData);
        }

        Debug.Log("Creating file \"" + totalPathString + "\" with behavior data.");


    }

    /*--------------------------------------------------------
     * This method is used to set up the conditions and details
     * for the next day.  This method is called when the player
     * sleeps on the bed for the next day.
     * ------------------------------------------------------*/
    public void ImportNextDayState()
    {
        Debug.Log("Testing File loading");

        // load behavior data

        string nextDayFileName = "NextDay.json";
        string totalPathString = System.IO.Path.Combine(pathString, nextDayFileName);
        string nextDayData;
        //= JsonConvert.SerializeObject(NPCsettings); // converts data in the NPCsettings object into a string

        if (!System.IO.File.Exists(totalPathString))
        {
            Debug.Log("Error, file \"" + totalPathString + "\" not found.");
        }
        else
        {
            using (System.IO.FileStream fs = System.IO.File.OpenRead(totalPathString))
            {
                using var sr = new StreamReader(fs);

                nextDayData = sr.ReadToEnd();
            }

            Debug.Log("Loading behavior data from file \"" + totalPathString + "\".");

            saveFile = JsonConvert.DeserializeObject<SaveFile>(nextDayData);

            Debug.Log("Setting up NPCBehaviorSetting object.");

        }
        

        
    }

    public void ExportSaveData(string savename)
    {
        Debug.Log("Testing File saving");

        // save next day data

        string savesFolderPath = Path.Combine(pathString, "saves");
        string totalPathString = System.IO.Path.Combine(savesFolderPath, savename + ".json");
        string saveData = JsonConvert.SerializeObject(saveFile); // converts data in the NPCsettings object into a string

        System.IO.Directory.CreateDirectory(pathString);

        if (System.IO.File.Exists(totalPathString))
        {
            Debug.Log("Overwriting file \"" + totalPathString + "\".");
        }
        
        using (System.IO.FileStream fs = System.IO.File.Create(totalPathString))
        {
            using var sr = new StreamWriter(fs);

            sr.Write(saveData);
        }

        Debug.Log("Creating file \"" + totalPathString + "\" with behavior data.");

        // save achievement data

        string achievementFileName = "AchievementData.json";
        totalPathString = System.IO.Path.Combine(pathString, achievementFileName);
        string achievementData = JsonConvert.SerializeObject(achievementSettings.Achievements); // converts data in the NPCsettings object into a string

        System.IO.Directory.CreateDirectory(pathString);

        if (System.IO.File.Exists(totalPathString))
        {
            Debug.Log("Overwriting file \"" + totalPathString + "\".");
        }

        using (System.IO.FileStream fs = System.IO.File.Create(totalPathString))
        {
            using var sr = new StreamWriter(fs);

            sr.Write(achievementData);
        }

        Debug.Log("Creating file \"" + totalPathString + "\" with key data.");

    }

    public void ImportSaveData(string savename)
    {
        var savesFolder = System.IO.Path.Combine(pathString, "saves");
        var saveFile = System.IO.Path.Combine(savesFolder, savename + ".json");

        if(!File.Exists(saveFile))
        {
            return;
        }

        FileStream fileStream = File.OpenRead(saveFile);
        StreamReader fileReader = new(fileStream);
        string fileData = fileReader.ReadToEnd();
        fileReader.Dispose();
        fileStream.Dispose();

        this.saveFile = JsonConvert.DeserializeObject<SaveFile>(fileData);
    }

    public void ImportAchievements()
    {
        var saveFile = System.IO.Path.Combine(pathString, "AchievementData.json");

        if(!File.Exists(saveFile))
        {
            return;
        }

        FileStream fileStream = File.OpenRead(saveFile);
        StreamReader fileReader = new(fileStream);
        string fileData = fileReader.ReadToEnd();
        fileReader.Dispose();
        fileStream.Dispose();

        this.achievementSettings.Achievements = JsonConvert.DeserializeObject<AchievementSave[]>(fileData);
    }

    /// <summary>
    /// This method gets the behavior settings for the NPCs
    /// and stores them into the NPCsettings object
    /// </summary>
    public void CreateBehaviorSettings()
    {
        var behaviorMaker = new NPCBehaviorSetting();
        behaviorMaker.getAllBehaviorSettings(npcList);
        saveFile.NPCBehaviorSettings = behaviorMaker.behaviorSetting;
    }

    /// <summary>
    /// This method takes the data in the NPCsettings object
    /// using it to set the behaviors of the NPCs.
    /// </summary>
    public void SetBehaviorSettings()
    {
        var behaviorSetter = new NPCBehaviorSetting();
        behaviorSetter.behaviorSetting = saveFile.NPCBehaviorSettings;
        behaviorSetter.setAllBehaviorSettings(npcList);
    }

    /// <summary>
    /// This method gets the position settings for the NPCs and 
    /// player and stores them into the positionSettings object
    /// </summary>
    public void CreatePositionSettings()
    {
        var positionSettings = new PositionSettings();
        positionSettings.GetAllPositionSettings(npcList);
        positionSettings.GetPlayerPositionSettings(player);
        saveFile.PositionSettings = positionSettings.convertToFloats();
    }

    /// <summary>
    /// This method uses the data in the positionSettings object 
    /// and sets the positions of the NPCs and players.
    /// </summary>
    public void SetPositionSettings()
    {
        var positionSettings = saveFile.PositionSettings.convertToVector3();
        positionSettings.SetAllPositionSettings(npcList);
        positionSettings.SetPlayerPositionSettings(player);
    }

    /*--------------------------------------------------------
     * This method gets the light settings for lights, campfires,
     * and boomboxes in the game and stores them into the
     * lampSettings object
     * ------------------------------------------------------*/
    public void CreateLightSettings()
    {
        var lampSettings = new LightSettings();
        lampSettings.GetAllLightSettings(lampList);
        saveFile.LightSettings = lampSettings.lightSettings;
    }

    /*--------------------------------------------------------
     * This method uses the data in the lampSettings object 
     * and sets the status for the lights, campfires and boomboxes
     * in the game.
     * ------------------------------------------------------*/
    public void SetLightSettings()
    {
        var lampSettings = new LightSettings();
        lampSettings.lightSettings = saveFile.LightSettings;
        lampSettings.SetAllLightSettings(lampList);
    }

    /*--------------------------------------------------------
     * This method gets the active status for paper, pens and 
     * keys in the game and saves those into the paperSettings,
     * penSettings and keySettings objects
     * ------------------------------------------------------*/
    public void CreateObjectSettings()
    {
        var objectSettings = new PickableObjectSettings();
        objectSettings.GetAllObjectSettings(paperList);
        saveFile.PaperSettings = objectSettings.objectSettings;
        objectSettings.GetAllObjectSettings(penList);
        saveFile.PenSettings = objectSettings.objectSettings;
        objectSettings.GetAllObjectSettings(keyList);
        saveFile.KeySettings = objectSettings.objectSettings;

    }
    /*--------------------------------------------------------
     * This method uses the data in the paperSettings, penSettings,
     * and keySettings objects and uses that to set the active
     * status for paper, pens and keys in the game.
     * ------------------------------------------------------*/
    public void SetObjectSettings()
    {
        var objectSettings = new PickableObjectSettings();
        objectSettings.objectSettings = saveFile.PaperSettings;
        objectSettings.SetAllObjectSettings(paperList);
        objectSettings.objectSettings = saveFile.PenSettings;
        objectSettings.SetAllObjectSettings(penList);
        objectSettings.objectSettings = saveFile.KeySettings;
        objectSettings.SetAllObjectSettings(keyList);
    }

    /*--------------------------------------------------------
     * This method gets the active status for achievements in
     * the game and saves those into achievementSettings object
     * ------------------------------------------------------*/

    public void CreateAchievementSettings()
    {
        achievementSettings.LoadAchievementsFromGame();
    }

    public void SetAchievementSettings()
    {
        achievementSettings.LoadAchievementFromSave();
    }

    public void CreateInventory()
    {
        saveFile.PensHeld = player.invScript.Pens;
        saveFile.PaperHeld = player.invScript.Paper;
        saveFile.LettersHeld = player.invScript.Letters.ToArray();
    }

    public void SetInventory()
    {
        player.invScript.Pens = saveFile.PensHeld;
        player.invScript.Paper = saveFile.PaperHeld;
        player.invScript.inventoryObjects = new List<GameObject>();
        player.invScript.inventoryNames = new List<string>();
        for(int i = 0; i < keyList.Length; i++)
        {
            if(saveFile.KeySettings[i])
            {
                player.invScript.inventoryNames.Add(keyList[i].GetComponent<KeyScript>().objectName);
                player.invScript.inventoryObjects.Add(keyList[i]);
            }
        }
        player.invScript.Letters = saveFile.LettersHeld.ToList();
    }

    public void CreateLog()
    {
        var logList = GameObject.FindObjectOfType<Log>();
        saveFile.Logs = logList.LogList.ToArray();
        saveFile.LogsCounter = logList.Counter;
    }
    public void SetLog()
    {
        var logList = GameObject.FindObjectOfType<Log>();
        logList.LogList = saveFile.Logs.ToList();
        logList.Counter = saveFile.LogsCounter;
    }

    public void NextDayUpdates()
    {
        LoadNextDayState();
        ResupplyPaper(Clock.timeToNextDay().Hour);  //add paper based on the time that passes to next morning
        ResupplyPens(Clock.timeToNextDay().Hour);   //add pens based on the time that passes to next morning
        player.ReduceSuspicion(Clock.timeToNextDay().Hour * 60 + Clock.timeToNextDay().Minute); // reduce suspicion
        Clock.Time = Clock.nextDayTime();
    }

    // currently being used for testing purposes
    public void UpdateClock(ClockTime time)
    {
        /*
        if (time.Hour==6 && time.Minute==45)
        {
            Debug.Log("Getting behaviors, positions, light settings.");
            CreateNextDayState();
            ExportNextDayState();
        }
        */

        if (time.Hour == 6 && time.Minute == 51)
        {
            player.PlayAudioClip(player._playerSounds.Rooster);
        }

        if (time.Hour == 22 && time.Minute == 0)
        {
            player.PlayAudioClip(player._playerSounds.Night);
        }
        
    }

    /*--------------------------------------------------------
     * This method will reactivate up to num pieces of paper in
     * the game in a random manner
     * ------------------------------------------------------*/
    public void ResupplyPaper(int num)
    {
        int randomValue;
        for (int i=0; i < num; i++)
        {
            randomValue = UnityEngine.Random.Range(0, paperList.Length);
            paperList[randomValue].SetActive(true);
        }
    }

    /*--------------------------------------------------------
    * This method will reactivate up to num pens in
    * the game in a random manner
    * ------------------------------------------------------*/
    public void ResupplyPens(int num)
    {
        int randomValue;
        for (int i = 0; i < num; i++)
        {
            randomValue = UnityEngine.Random.Range(0, penList.Length);
            penList[randomValue].SetActive(true);
        }
    }

    private void SavePlayerName()
    {
        saveFile.PlayerName = Player.Name;
    }

    private void LoadPlayerName()
    {
        Player.Name = saveFile.PlayerName;
    }

    private void SaveClockTime()
    {
        saveFile.Time = new ClockTime(Clock.Time);
    }

    public void LoadClockTime()
    {
        Clock.Time = new ClockTime(saveFile.Time);
    }

    public void SaveKnownPeople()
    {
        saveFile.PeopleKnown = player.PeopleKnown;
    }

    public void LoadKnownPeople()
    {
        player.PeopleKnown = saveFile.PeopleKnown;
    }
}
