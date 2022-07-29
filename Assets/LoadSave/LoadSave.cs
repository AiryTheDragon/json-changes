using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class LoadSave : MonoBehaviour, INeedsClockUpdate
{

    public NPCBehavior[] npcList;
    public NPCBehaviorSetting NPCsettings;
    public PositionSettings positionSettings;
    public LampBehavior[] lampList;
    public LightSettings lampSettings;
    public GameObject[] paperList;
    public GameObject[] penList;
    public GameObject[] keyList;
    public GameObject[] miscList; // TODO
    public PickableObjectSettings paperSettings;
    public PickableObjectSettings penSettings;
    public PickableObjectSettings keySettings;
    public PickableObjectSettings miscSettings; // TODO
    public InvScript invSettings; // TODO
    public BannedActivitiesBehavior bannedActivitiesSettings; // TODO
    public Log logSettings; // TODO
    public ClockBehavior Clock;
    public Player player;
    private string fileFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
    private string subpathString = "EEData";
    private string pathString;


    public void Start()
    {
        pathString = System.IO.Path.Combine(fileFolder, subpathString);

        NPCsettings = NPCBehaviorSetting.CreateInstance<NPCBehaviorSetting>();
        positionSettings = PositionSettings.CreateInstance<PositionSettings>();
        lampSettings = LightSettings.CreateInstance<LightSettings>();
        paperSettings = PickableObjectSettings.CreateInstance<PickableObjectSettings>();
        penSettings = PickableObjectSettings.CreateInstance<PickableObjectSettings>();
        keySettings = PickableObjectSettings.CreateInstance<PickableObjectSettings>();
        miscSettings = PickableObjectSettings.CreateInstance<PickableObjectSettings>();

        npcList = GameObject.FindObjectsOfType<NPCBehavior>();
        lampList = GameObject.FindObjectsOfType<LampBehavior>();
        paperList = GameObject.FindGameObjectsWithTag("Paper");
        penList = GameObject.FindGameObjectsWithTag("Pen");
        keyList = GameObject.FindGameObjectsWithTag("Key");

        Clock.NeedsClockUpdate.Add(this);
    }

    public void CreateSaveData()
    {
        CreateBehaviorSettings();
        //TODO save guard patrol settings
        CreatePositionSettings();
        CreateLightSettings();
        CreateObjectSettings();
        //TODO Save inventory
        //TODO Save log
    }

    public void LoadSaveData()
    {
        SetBehaviorSettings();
        //TODO load guard patrol settings
        SetPositionSettings();
        SetLightSettings();
        SetObjectSettings();
        //TODO Load inventory
        //TODO Load log
    }

    public void CreateNextDayState()
    {
        CreateBehaviorSettings();
        //TODO save guard patrol settings
        CreatePositionSettings();
        CreateLightSettings();
    }

    public void LoadNextDayState()
    {
        //TODO load NextDayState()
        SetBehaviorSettings();
        SetPositionSettings();
        SetLightSettings();
        //TODO update paper, add paper to game based on time slept
        //TODO update pens, add pens to game based on time slept
        //TODO update suspecion, based on time slept.
        //TODO set clock

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
     * 
     * 
     * 
     * 
     * 
     * ------------------------------------------------------*/
    public void ExportNextDayState()
    {
        Debug.Log("Testing File saving");

        // save behavior data

        string behaviorFileName = "BehaviorData.json";
        string totalPathString = System.IO.Path.Combine(pathString, behaviorFileName);
        string behaviorData = JsonConvert.SerializeObject(NPCsettings); // converts data in the NPCsettings object into a string

        System.IO.Directory.CreateDirectory(pathString);

        if (System.IO.File.Exists(totalPathString))
        {
            Debug.Log("Overwriting file \"" + totalPathString + "\".");
        }
        
        using (System.IO.FileStream fs = System.IO.File.Create(totalPathString))
        {
            using var sr = new StreamWriter(fs);

            sr.Write(behaviorData);
        }

        Debug.Log("Creating file \"" + totalPathString + "\" with behavior data.");

        // save position data

        string positionFileName = "PositionData.json";
        totalPathString = System.IO.Path.Combine(pathString, positionFileName);

        PositionSettingsConversion posSetConv = positionSettings.convertToFloats(); // converts positions into easier to serialize object

        string positionData = JsonConvert.SerializeObject(posSetConv); // converts position data into a string

        if (System.IO.File.Exists(totalPathString))
        {
            Debug.Log("Overwriting file \"" + totalPathString + "\".");
        }
 
        using (System.IO.FileStream fs = System.IO.File.Create(totalPathString))
        {
            using var sr = new StreamWriter(fs);

            sr.Write(positionData);
        }

        Debug.Log("Creating file \"" + totalPathString + "\" with position data.");

        // save light data

        string lightFileName = "LightData.json";
        totalPathString = System.IO.Path.Combine(pathString, lightFileName);
        string lightData = JsonConvert.SerializeObject(lampSettings); // converts data in the NPCsettings object into a string

        System.IO.Directory.CreateDirectory(pathString);

        if (System.IO.File.Exists(totalPathString))
        {
            Debug.Log("Overwriting file \"" + totalPathString + "\".");
        }

        using (System.IO.FileStream fs = System.IO.File.Create(totalPathString))
        {
            using var sr = new StreamWriter(fs);

            sr.Write(lightData);
        }

        Debug.Log("Creating file \"" + totalPathString + "\" with light data.");

    }

    public void ImportNextDayState()
    {
        Debug.Log("Testing File loading");

        // load behavior data

        string behaviorFileName = "BehaviorData.json";
        string totalPathString = System.IO.Path.Combine(pathString, behaviorFileName);
        string behaviorData;
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

                behaviorData = sr.ReadToEnd();
            }

            Debug.Log("Loading behavior data from file \"" + totalPathString + "\".");

            NPCsettings = JsonConvert.DeserializeObject<NPCBehaviorSetting>(behaviorData);

            Debug.Log("Setting up NPCBehaviorSetting object.");

        }
        // load position data

        string positionFileName = "PositionData.json";
        totalPathString = System.IO.Path.Combine(pathString, positionFileName);
        string positionDataFloat;
        if (!System.IO.File.Exists(totalPathString))
        {
            Debug.Log("Error, file \"" + totalPathString + "\" not found.");
        }
        else
        {
            using (System.IO.FileStream fs = System.IO.File.OpenRead(totalPathString))
            {
                using var sr = new StreamReader(fs);

                positionDataFloat = sr.ReadToEnd();
            }

            Debug.Log("Loading position data from file \"" + totalPathString + "\".");

            PositionSettingsConversion posSetConv = JsonConvert.DeserializeObject<PositionSettingsConversion>(positionDataFloat);
            positionSettings = posSetConv.convertToVector3();

            Debug.Log("Setting up PositionSetting object.");

        } 

        // save light data

        string lightFileName = "LightData.json";
        totalPathString = System.IO.Path.Combine(pathString, lightFileName);
        string lightData;
        //= JsonConvert.SerializeObject(lampSettings); // converts data in the NPCsettings object into a string

        System.IO.Directory.CreateDirectory(pathString);

        if (!System.IO.File.Exists(totalPathString))
        {
            Debug.Log("Error, file \"" + totalPathString + "\" not found.");
        }
        else
        {
            using (System.IO.FileStream fs = System.IO.File.OpenRead(totalPathString))
            {
                using var sr = new StreamReader(fs);

                lightData = sr.ReadToEnd();
            }

            Debug.Log("Loading position data from file \"" + totalPathString + "\".");
            lampSettings = JsonConvert.DeserializeObject<LightSettings>(lightData);

        }
    }

    public void ExportSaveData()
    {
        Debug.Log("Exporting Data");
        Debug.Log("Testing File saving");

        // save behavior data

        string behaviorFileName = "BehaviorData.json";
        string totalPathString = System.IO.Path.Combine(pathString, behaviorFileName);
        string behaviorData = JsonConvert.SerializeObject(NPCsettings); // converts data in the NPCsettings object into a string

        System.IO.Directory.CreateDirectory(pathString);

        if (System.IO.File.Exists(totalPathString))
        {
            Debug.Log("Overwriting file \"" + totalPathString + "\".");
        }

        using (System.IO.FileStream fs = System.IO.File.Create(totalPathString))
        {
            using var sr = new StreamWriter(fs);

            sr.Write(behaviorData);
        }

        Debug.Log("Creating file \"" + totalPathString + "\" with behavior data.");

        // save position data

        string positionFileName = "PositionData.json";
        totalPathString = System.IO.Path.Combine(pathString, positionFileName);

        PositionSettingsConversion posSetConv = positionSettings.convertToFloats(); // converts positions into easier to serialize object

        string positionData = JsonConvert.SerializeObject(posSetConv); // converts position data into a string

        if (System.IO.File.Exists(totalPathString))
        {
            Debug.Log("Overwriting file \"" + totalPathString + "\".");
        }

        using (System.IO.FileStream fs = System.IO.File.Create(totalPathString))
        {
            using var sr = new StreamWriter(fs);

            sr.Write(positionData);
        }

        Debug.Log("Creating file \"" + totalPathString + "\" with position data.");

        // save light data

        string lightFileName = "LightData.json";
        totalPathString = System.IO.Path.Combine(pathString, lightFileName);
        string lightData = JsonConvert.SerializeObject(lampSettings); // converts data in the NPCsettings object into a string

        System.IO.Directory.CreateDirectory(pathString);

        if (System.IO.File.Exists(totalPathString))
        {
            Debug.Log("Overwriting file \"" + totalPathString + "\".");
        }

        using (System.IO.FileStream fs = System.IO.File.Create(totalPathString))
        {
            using var sr = new StreamWriter(fs);

            sr.Write(lightData);
        }

        Debug.Log("Creating file \"" + totalPathString + "\" with light data.");

        // save paper data

        string paperFileName = "PaperData.json";
        totalPathString = System.IO.Path.Combine(pathString, paperFileName);
        string paperData = JsonConvert.SerializeObject(paperSettings); // converts data in the NPCsettings object into a string

        System.IO.Directory.CreateDirectory(pathString);

        if (System.IO.File.Exists(totalPathString))
        {
            Debug.Log("Overwriting file \"" + totalPathString + "\".");
        }

        using (System.IO.FileStream fs = System.IO.File.Create(totalPathString))
        {
            using var sr = new StreamWriter(fs);

            sr.Write(paperData);
        }

        Debug.Log("Creating file \"" + totalPathString + "\" with paper data.");

        // save pen data

        string penFileName = "PenData.json";
        totalPathString = System.IO.Path.Combine(pathString, penFileName);
        string penData = JsonConvert.SerializeObject(penSettings); // converts data in the NPCsettings object into a string

        System.IO.Directory.CreateDirectory(pathString);

        if (System.IO.File.Exists(totalPathString))
        {
            Debug.Log("Overwriting file \"" + totalPathString + "\".");
        }

        using (System.IO.FileStream fs = System.IO.File.Create(totalPathString))
        {
            using var sr = new StreamWriter(fs);

            sr.Write(penData);
        }

        Debug.Log("Creating file \"" + totalPathString + "\" with pen data.");

        // save key data

        string keyFileName = "KeyData.json";
        totalPathString = System.IO.Path.Combine(pathString, keyFileName);
        string keyData = JsonConvert.SerializeObject(keySettings); // converts data in the NPCsettings object into a string

        System.IO.Directory.CreateDirectory(pathString);

        if (System.IO.File.Exists(totalPathString))
        {
            Debug.Log("Overwriting file \"" + totalPathString + "\".");
        }

        using (System.IO.FileStream fs = System.IO.File.Create(totalPathString))
        {
            using var sr = new StreamWriter(fs);

            sr.Write(keyData);
        }

        Debug.Log("Creating file \"" + totalPathString + "\" with key data.");

    }

    public void ImportSaveData()
    {

    }


    public void CreateBehaviorSettings()
    {
        NPCsettings.getAllBehaviorSettings(npcList);
    }

    public void SetBehaviorSettings()
    {
        NPCsettings.setAllBehaviorSettings(npcList);
    }

    public void CreatePositionSettings()
    {
        positionSettings.getAllPositionSettings(npcList);
        positionSettings.getPlayerPositionSettings(player);
    }

    public void SetPositionSettings()
    {
        positionSettings.setAllPositionSettings(npcList);
        positionSettings.setPlayerPositionSettings(player);
    }

    public void CreateLightSettings()
    {
        lampSettings.getAllLightSettings(lampList);
    }

    public void SetLightSettings()
    {
        lampSettings.setAllLightSettings(lampList);
    }

    public void CreateObjectSettings()
    {
        paperSettings.getAllObjectSettings(paperList);
        penSettings.getAllObjectSettings(penList);
        keySettings.getAllObjectSettings(keyList);

    }

    public void SetObjectSettings()
    {
        paperSettings.setAllObjectSettings(paperList);
        penSettings.setAllObjectSettings(penList);
        keySettings.setAllObjectSettings(keyList);
    }

    // currently being used for testing purposes
    public void UpdateClock(ClockTime time)
    {
        if (time.Hour==5 && time.Minute==0)
        {
            Debug.Log("Getting behaviors, positions, light settings.");
            CreateNextDayState();
            ExportNextDayState();
        }

        
        /*
        if (time.Hour==6 && time.Minute==0)
        {
            Debug.Log("Setting behaviors, positions, lights.");
            ImportNextDayState();
            LoadNextDayState();
        }
        */
        
    }
}
