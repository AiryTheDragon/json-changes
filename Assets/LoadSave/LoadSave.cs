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
    private GameObject[] bookList;
    private GameObject[] miscList; // TODO

    private readonly AchievementSettings achievementSettings = new();

    public InvScript invSettings; // TODO
    public BannedActivitiesBehavior bannedActivitiesSettings; // TODO
    public Log logSettings; // TODO
    public ClockBehavior Clock;
    public Player player;

    private SaveFile saveFile;

    private string fileFolder;
    private string subpathString = "EEData";
    private string pathString;


    public void Start()
    {
        fileFolder = Application.persistentDataPath;
        pathString = System.IO.Path.Combine(fileFolder, subpathString);

        saveFile = new SaveFile();

        npcList = GameObject.FindObjectsOfType<NPCBehavior>();
        lampList = GameObject.FindObjectsOfType<LampBehavior>();
        paperList = GameObject.FindGameObjectsWithTag("Paper");
        penList = GameObject.FindGameObjectsWithTag("Pen");
        keyList = GameObject.FindGameObjectsWithTag("Key");
        bookList = GameObject.FindGameObjectsWithTag("FreedomBook");
        player = GameObject.FindObjectOfType<Player>();



        if(string.IsNullOrWhiteSpace(Player.Name))
        {
            ImportSaveData($"save{Player.SaveFile}");
            ImportAchievements();
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
        if(saveFile is null)
        {
            saveFile = new();
        }
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
        /*
        string nextDayData = @"{""PlayerName"":null,""Time"":null,""NPCBehaviorSettings"":
[{""NPCName"":""Mike"",""groupOfActivitiesName"":""Mike's Night Activities"",""activityPos"":9,""actionPos"":5,""actionInfo"":{""type"":13,""endTime"":{""Hour"":6,""Minute"":58,""Day"":0,""Week"":0,""Year"":0,""MonthOfYear"":0,""WeekOfYear"":0,""DayOfWeek"":0,""DayOfMonth"":0,""DayOfYear"":0},""data"":null,""name"":"""",""hideFlags"":0},""isGuard"":false,""isPatrolling"":false,""name"":"""",""hideFlags"":0},{""NPCName"":""Helpful Phil"",""groupOfActivitiesName"":""Phil's Daytime Activities"",""activityPos"":1,""actionPos"":38,""actionInfo"":{""type"":13,""endTime"":{""Hour"":6,""Minute"":48,""Day"":0,""Week"":0,""Year"":0,""MonthOfYear"":0,""WeekOfYear"":0,""DayOfWeek"":0,""DayOfMonth"":0,""DayOfYear"":0},""data"":null,""name"":"""",""hideFlags"":0},""isGuard"":false,""isPatrolling"":false,""name"":"""",""hideFlags"":0},{""NPCName"":""Sassy Sam"",""groupOfActivitiesName"":""Sam's Night Activities"",""activityPos"":4,""actionPos"":1,""actionInfo"":{""type"":13,""endTime"":{""Hour"":7,""Minute"":26,""Day"":0,""Week"":0,""Year"":0,""MonthOfYear"":0,""WeekOfYear"":0,""DayOfWeek"":0,""DayOfMonth"":0,""DayOfYear"":0},""data"":null,""name"":"""",""hideFlags"":0},""isGuard"":false,""isPatrolling"":false,""name"":"""",""hideFlags"":0},{""NPCName"":""Jade"",""groupOfActivitiesName"":""Jade's Night Activities"",""activityPos"":2,""actionPos"":1,""actionInfo"":{""type"":13,""endTime"":{""Hour"":7,""Minute"":36,""Day"":0,""Week"":0,""Year"":0,""MonthOfYear"":0,""WeekOfYear"":0,""DayOfWeek"":0,""DayOfMonth"":0,""DayOfYear"":0},""data"":null,""name"":"""",""hideFlags"":0},""isGuard"":false,""isPatrolling"":false,""name"":"""",""hideFlags"":0},{""NPCName"":""Dee"",""groupOfActivitiesName"":""Dee's Daytime Activities"",""activityPos"":0,""actionPos"":2,""actionInfo"":{""type"":13,""endTime"":{""Hour"":6,""Minute"":46,""Day"":0,""Week"":0,""Year"":0,""MonthOfYear"":0,""WeekOfYear"":0,""DayOfWeek"":0,""DayOfMonth"":0,""DayOfYear"":0},""data"":null,""name"":"""",""hideFlags"":0},""isGuard"":true,""isPatrolling"":true,""name"":"""",""hideFlags"":0},{""NPCName"":""Joe (Unknown)"",""groupOfActivitiesName"":""Hobo Joe's Night Activities"",""activityPos"":8,""actionPos"":0,""actionInfo"":{""type"":15,""endTime"":null,""data"":null,""name"":"""",""hideFlags"":0},""isGuard"":false,""isPatrolling"":false,""name"":"""",""hideFlags"":0},{""NPCName"":""Slowpoke"",""groupOfActivitiesName"":""Slowpoke's Day Activities"",""activityPos"":0,""actionPos"":0,""actionInfo"":{""type"":15,""endTime"":null,""data"":null,""name"":"""",""hideFlags"":0},""isGuard"":true,""isPatrolling"":true,""name"":"""",""hideFlags"":0},{""NPCName"":""Jessie"",""groupOfActivitiesName"":""Jessie's Night Activities"",""activityPos"":1,""actionPos"":7,""actionInfo"":{""type"":15,""endTime"":null,""data"":null,""name"":"""",""hideFlags"":0},""isGuard"":true,""isPatrolling"":true,""name"":"""",""hideFlags"":0},{""NPCName"":""Isaac"",""groupOfActivitiesName"":""Isaac's Night Activities"",""activityPos"":9,""actionPos"":1,""actionInfo"":{""type"":13,""endTime"":{""Hour"":7,""Minute"":41,""Day"":0,""Week"":0,""Year"":0,""MonthOfYear"":0,""WeekOfYear"":0,""DayOfWeek"":0,""DayOfMonth"":0,""DayOfYear"":0},""data"":null,""name"":"""",""hideFlags"":0},""isGuard"":false,""isPatrolling"":false,""name"":"""",""hideFlags"":0},{""NPCName"":""Autumn"",""groupOfActivitiesName"":""Autumn's Night Activities"",""activityPos"":10,""actionPos"":1,""actionInfo"":{""type"":13,""endTime"":{""Hour"":7,""Minute"":25,""Day"":0,""Week"":0,""Year"":0,""MonthOfYear"":0,""WeekOfYear"":0,""DayOfWeek"":0,""DayOfMonth"":0,""DayOfYear"":0},""data"":null,""name"":"""",""hideFlags"":0},""isGuard"":false,""isPatrolling"":false,""name"":"""",""hideFlags"":0},{""NPCName"":""Bob"",""groupOfActivitiesName"":""GuardBossKey"",""activityPos"":0,""actionPos"":26,""actionInfo"":{""type"":13,""endTime"":{""Hour"":6,""Minute"":45,""Day"":0,""Week"":0,""Year"":0,""MonthOfYear"":0,""WeekOfYear"":0,""DayOfWeek"":0,""DayOfMonth"":0,""DayOfYear"":0},""data"":null,""name"":"""",""hideFlags"":0},""isGuard"":true,""isPatrolling"":true,""name"":"""",""hideFlags"":0},{""NPCName"":""Maxxy JJ"",""groupOfActivitiesName"":""Max's Night Activities"",""activityPos"":8,""actionPos"":1,""actionInfo"":{""type"":13,""endTime"":{""Hour"":7,""Minute"":3,""Day"":0,""Week"":0,""Year"":0,""MonthOfYear"":0,""WeekOfYear"":0,""DayOfWeek"":0,""DayOfMonth"":0,""DayOfYear"":0},""data"":null,""name"":"""",""hideFlags"":0},""isGuard"":false,""isPatrolling"":false,""name"":"""",""hideFlags"":0},{""NPCName"":""Manager George"",""groupOfActivitiesName"":""George's Night Activities"",""activityPos"":8,""actionPos"":1,""actionInfo"":{""type"":15,""endTime"":null,""data"":null,""name"":"""",""hideFlags"":0},""isGuard"":false,""isPatrolling"":false,""name"":"""",""hideFlags"":0},{""NPCName"":""Slinky"",""groupOfActivitiesName"":""Patrolling the player house"",""activityPos"":0,""actionPos"":3,""actionInfo"":{""type"":15,""endTime"":null,""data"":null,""name"":"""",""hideFlags"":0},""isGuard"":true,""isPatrolling"":true,""name"":"""",""hideFlags"":0},{""NPCName"":""Karon"",""groupOfActivitiesName"":""Patrol the sidewalk"",""activityPos"":0,""actionPos"":2,""actionInfo"":{""type"":15,""endTime"":null,""data"":null,""name"":"""",""hideFlags"":0},""isGuard"":true,""isPatrolling"":true,""name"":"""",""hideFlags"":0},{""NPCName"":""Sarah"",""groupOfActivitiesName"":""Sarah's Daytime1 Activities"",""activityPos"":4,""actionPos"":16,
""actionInfo"":{""type"":13,""endTime"":{""Hour"":6,""Minute"":46,""Day"":0,""Week"":0,""Year"":0,""MonthOfYear"":0,""WeekOfYear"":0,""DayOfWeek"":0,""DayOfMonth"":0,""DayOfYear"":0},""data"":null,""name"":"""",""hideFlags"":0},""isGuard"":false,""isPatrolling"":false,""name"":"""",""hideFlags"":0},{""NPCName"":""Donald"",""groupOfActivitiesName"":""Mike's Night Activities"",""activityPos"":9,""actionPos"":5,""actionInfo"":{""type"":13,""endTime"":{""Hour"":6,""Minute"":52,""Day"":0,""Week"":0,""Year"":0,""MonthOfYear"":0,""WeekOfYear"":0,""DayOfWeek"":0,""DayOfMonth"":0,""DayOfYear"":0},""data"":null,""name"":"""",""hideFlags"":0},""isGuard"":false,""isPatrolling"":false,""name"":"""",""hideFlags"":0},{""NPCName"":""Daniel"",""groupOfActivitiesName"":""Daniel's Night Activities"",""activityPos"":9,""actionPos"":1,""actionInfo"":{""type"":13,""endTime"":{""Hour"":7,""Minute"":5,""Day"":0,""Week"":0,""Year"":0,""MonthOfYear"":0,""WeekOfYear"":0,""DayOfWeek"":0,""DayOfMonth"":0,""DayOfYear"":0},""data"":null,""name"":"""",""hideFlags"":0},""isGuard"":false,""isPatrolling"":false,""name"":"""",""hideFlags"":0},{""NPCName"":""Dum"",""groupOfActivitiesName"":""Dum's Daytime Activities"",""activityPos"":0,""actionPos"":3,""actionInfo"":{""type"":15,""endTime"":null,""data"":null,""name"":"""",""hideFlags"":0},""isGuard"":true,""isPatrolling"":true,""name"":"""",""hideFlags"":0},{""NPCName"":""Pear"",""groupOfActivitiesName"":""Pear's Daytime Activities"",""activityPos"":0,""actionPos"":20,""actionInfo"":{""type"":15,""endTime"":null,""data"":null,""name"":"""",""hideFlags"":0},""isGuard"":true,""isPatrolling"":true,""name"":"""",""hideFlags"":0},{""NPCName"":""Airy"",""groupOfActivitiesName"":""Airy's Night Activities"",""activityPos"":2,""actionPos"":47,""actionInfo"":{""type"":15,""endTime"":null,""data"":null,""name"":"""",""hideFlags"":0},""isGuard"":false,""isPatrolling"":false,""name"":"""",""hideFlags"":0},{""NPCName"":""Onna"",""groupOfActivitiesName"":""Onna's Night Activities"",""activityPos"":15,""actionPos"":1,""actionInfo"":{""type"":13,""endTime"":{""Hour"":6,""Minute"":54,""Day"":0,""Week"":0,""Year"":0,""MonthOfYear"":0,""WeekOfYear"":0,""DayOfWeek"":0,""DayOfMonth"":0,""DayOfYear"":0},""data"":null,""name"":"""",""hideFlags"":0},""isGuard"":false,""isPatrolling"":false,""name"":"""",""hideFlags"":0},{""NPCName"":""Frank"",""groupOfActivitiesName"":""Escort NPC Male"",""activityPos"":0,""actionPos"":3,""actionInfo"":{""type"":6,""endTime"":null,""data"":""Airy"",""name"":"""",""hideFlags"":0},""isGuard"":true,""isPatrolling"":false,""name"":"""",""hideFlags"":0},{""NPCName"":""Tiny"",""groupOfActivitiesName"":""Tiny's Night Activities"",""activityPos"":5,""actionPos"":1,""actionInfo"":{""type"":13,""endTime"":{""Hour"":7,""Minute"":33,""Day"":0,""Week"":0,""Year"":0,""MonthOfYear"":0,""WeekOfYear"":0,""DayOfWeek"":0,""DayOfMonth"":0,""DayOfYear"":0},""data"":null,""name"":"""",""hideFlags"":0},""isGuard"":true,""isPatrolling"":false,""name"":"""",""hideFlags"":0},{""NPCName"":""Bethany"",""groupOfActivitiesName"":""Bethany's Night Activities"",""activityPos"":12,""actionPos"":1,""actionInfo"":{""type"":13,""endTime"":{""Hour"":6,""Minute"":55,""Day"":0,""Week"":0,""Year"":0,""MonthOfYear"":0,""WeekOfYear"":0,""DayOfWeek"":0,""DayOfMonth"":0,""DayOfYear"":0},""data"":null,""name"":"""",""hideFlags"":0},""isGuard"":false,""isPatrolling"":false,""name"":"""",""hideFlags"":0},{""NPCName"":""Liberty"",""groupOfActivitiesName"":""Liberty's Daytime Activities"",""activityPos"":2,""actionPos"":9,""actionInfo"":{""type"":13,""endTime"":{""Hour"":6,""Minute"":47,""Day"":0,""Week"":0,""Year"":0,""MonthOfYear"":0,""WeekOfYear"":0,""DayOfWeek"":0,""DayOfMonth"":0,""DayOfYear"":0},""data"":null,""name"":"""",""hideFlags"":0},""isGuard"":false,""isPatrolling"":false,""name"":"""",""hideFlags"":0},{""NPCName"":""Jonathan"",""groupOfActivitiesName"":""Jon's Night Activities"",""activityPos"":11,""actionPos"":1,""actionInfo"":{""type"":13,""endTime"":{""Hour"":7,""Minute"":39,""Day"":0,""Week"":0,""Year"":0,""MonthOfYear"":0,""WeekOfYear"":0,""DayOfWeek"":0,""DayOfMonth"":0,""DayOfYear"":0},""data"":null,""name"":"""",""hideFlags"":0},""isGuard"":false,""isPatrolling"":false,""name"":"""",""hideFlags"":0},{""NPCName"":""Karen"",""groupOfActivitiesName"":""Karen's Night Activities"",""activityPos"":6,""actionPos"":3,""actionInfo"":{""type"":13,""endTime"":{""Hour"":6,""Minute"":49,""Day"":0,""Week"":0,""Year"":0,""MonthOfYear"":0,""WeekOfYear"":0,""DayOfWeek"":0,""DayOfMonth"":0,""DayOfYear"":0},""data"":null,""name"":"""",""hideFlags"":0},""isGuard"":false,""isPatrolling"":false,""name"":"""",""hideFlags"":0},{""NPCName"":""Deaf Jacob"",""groupOfActivitiesName"":""Daniel's Night Activities"",""activityPos"":6,""actionPos"":1,""actionInfo"":{""type"":13,""endTime"":{""Hour"":7,""Minute"":22,""Day"":0,""Week"":0,""Year"":0,""MonthOfYear"":0,""WeekOfYear"":0,""DayOfWeek"":0,""DayOfMonth"":0,""DayOfYear"":0},""data"":null,""name"":"""",""hideFlags"":0},""isGuard"":false,""isPatrolling"":false,""name"":"""",""hideFlags"":0}],""PositionSettings"":{""positionSettingsFloat"":[16.441185,25.7297211,-1.0,6.546749,-2.20869064,-2.0,7.44116974,-24.2524261,-1.0,15.9399376,83.736,-1.0,-19.94749,-68.72664,-2.0,-49.43396,-80.68292,-1.0,27.121048,-0.716712952,-2.0,0.252436638,-94.02751,-2.0,15.6965981,-49.2805939,-1.0,16.6900635,-49.25727,-1.0,31.0000114,-95.24902,-2.0,14.5221672,-24.3069763,-1.0,-30.2763519,-99.9118958,-2.0,4.0076623,-10.0230675,-2.0,-9.750047,-28.0567169,-2.0,-55.01893,-100.751953,-1.0,-50.4460258,24.2718525,-1.0,-45.9409256,-0.7622986,-1.0,-16.8432617,-70.25421,-2.0,-18.619091,-57.35339,-2.0,-18.7499123,-8.372704,-2.0,-50.4396057,53.5005951,-1.0,-18.7498741,-8.972706,-2.0,13.5133619,-88.55359,-2.0,-49.94953,-25.0237427,-1.0,13.5410957,53.7924271,-2.0,-50.2501869,-49.2499924,-1.0,-21.5587883,-86.269,-2.0,-77.9785843,91.19717,-1.0],""positionNames"":[""Mike"",""Helpful Phil"",""Sassy Sam"",""Jade"",""Dee"",""Joe (Unknown)"",""Slowpoke"",""Jessie"",""Isaac"",""Autumn"",""Bob"",""Maxxy JJ"",""Manager George"",""Slinky"",""Karon"",""Sarah"",""Donald"",""Daniel"",""Dum"",""Pear"",""Airy"",""Onna"",""Frank"",""Tiny"",""Bethany"",""Liberty"",""Jonathan"",""Karen"",""Deaf Jacob""],""playerPositionSettingsFloat"":[16.2862988,-0.623662353,0.0]},""LightSettings"":[false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false,false,true,false,false,false,false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,true,false,false,false,false,false,false,true,false,false,false,false],""PaperSettings"":null,""PenSettings"":null,""KeySettings"":null,""MiscSettings"":null,""PensHeld"":0,""PaperHeld"":0,""LettersHeld"":null,""Logs"":null,""LogsCounter"":0,""PeopleKnown"":null}";
        */
        string nextDayData = @"{ ""PlayerName"":null,""Time"":null,""NPCBehaviorSettings"":[{
            ""NPCName"":
""Mike"",""groupOfActivitiesName"":""Mike's Night Activities"",""activityPos"":9,
 ""actionPos"":1,""actionInfo"":{
                ""type"":13,""endTime"":{
                    ""Hour"":6,""Minute"":51,
""Day"":1,""Week"":0,""Year"":0,""MonthOfYear"":0,""WeekOfYear"":0,""DayOfWeek"":1,
""DayOfMonth"":1,""DayOfYear"":1},""data"":null},""isGuard"":false,
""isPatrolling"":false},{
            ""NPCName"":""Helpful Phil"",
""groupOfActivitiesName"":""Phil's Daytime Activities"",
""activityPos"":1,""actionPos"":54,""actionInfo"":{
                ""type"":13,
""endTime"":{
                    ""Hour"":6,""Minute"":50,""Day"":1,""Week"":0,""Year"":0,
""MonthOfYear"":0,""WeekOfYear"":0,""DayOfWeek"":1,""DayOfMonth"":1,
""DayOfYear"":1},""data"":null},""isGuard"":false,""isPatrolling"":false},
{
            ""NPCName"":""Sassy Sam"",""groupOfActivitiesName"":""Sam's Night Activities"",
""activityPos"":4,""actionPos"":1,""actionInfo"":{
                ""type"":13,
""endTime"":{
                    ""Hour"":7,""Minute"":14,""Day"":1,""Week"":0,""Year"":0,
""MonthOfYear"":0,""WeekOfYear"":0,""DayOfWeek"":1,""DayOfMonth"":1,
""DayOfYear"":1},""data"":null},""isGuard"":false,""isPatrolling"":false},
{
            ""NPCName"":""Jade"",""groupOfActivitiesName"":""Jade's Night Activities"",
""activityPos"":4,""actionPos"":7,""actionInfo"":{
                ""type"":13,""endTime"":
{
                    ""Hour"":6,""Minute"":52,""Day"":1,""Week"":0,""Year"":0,""MonthOfYear"":0,
""WeekOfYear"":0,""DayOfWeek"":1,""DayOfMonth"":1,""DayOfYear"":1},""data"":null},
""isGuard"":false,""isPatrolling"":false},{
            ""NPCName"":""Dee"",
""groupOfActivitiesName"":""Dee's Daytime Activities"",""activityPos"":0,
""actionPos"":2,""actionInfo"":{
                ""type"":13,""endTime"":{
                    ""Hour"":6,""Minute"":47,
""Day"":1,""Week"":0,""Year"":0,""MonthOfYear"":0,""WeekOfYear"":0,""DayOfWeek"":1,
""DayOfMonth"":1,""DayOfYear"":1},""data"":null},""isGuard"":true,""isPatrolling"":true},
{
            ""NPCName"":""Joe(Unknown)"",""groupOfActivitiesName"":""Hobo Joe's Night Activities"",
""activityPos"":11,""actionPos"":7,""actionInfo"":
{
                ""type"":13,""endTime"":{
                    ""Hour"":6,""Minute"":46,""Day"":1,""Week"":0,
""Year"":0,""MonthOfYear"":0,""WeekOfYear"":0,""DayOfWeek"":1,
""DayOfMonth"":1,""DayOfYear"":1},""data"":null},""isGuard"":false,
""isPatrolling"":false},{
            ""NPCName"":""Slowpoke"",""groupOfActivitiesName"":
""Slowpoke's Day Activities"",""activityPos"":0,""actionPos"":0,""actionInfo"":
{ ""type"":15,""endTime"":null,""data"":null},""isGuard"":true,""isPatrolling"":true},
{
            ""NPCName"":""Jessie"",""groupOfActivitiesName"":""Jessie's Night Activities"",
""activityPos"":4,""actionPos"":0,""actionInfo"":{
                ""type"":15,""endTime"":null,
""data"":null},""isGuard"":true,""isPatrolling"":false},{
            ""NPCName"":""Isaac"",
""groupOfActivitiesName"":""Isaac's Night Activities"",""activityPos"":10,
""actionPos"":1,""actionInfo"":{
                ""type"":13,""endTime"":{
                    ""Hour"":6,""Minute"":
49,""Day"":1,""Week"":0,""Year"":0,""MonthOfYear"":0,""WeekOfYear"":0,
""DayOfWeek"":1,""DayOfMonth"":1,""DayOfYear"":1},""data"":null},""isGuard"":false,
""isPatrolling"":false},{
            ""NPCName"":""Autumn"",""groupOfActivitiesName"":
""Autumn's Night Activities"",""activityPos"":10,""actionPos"":1,""actionInfo"":
{
                ""type"":13,""endTime"":{
                    ""Hour"":7,""Minute"":43,""Day"":1,""Week"":0,""Year"":0,
""MonthOfYear"":0,""WeekOfYear"":0,""DayOfWeek"":1,""DayOfMonth"":1,
""DayOfYear"":1},""data"":null},""isGuard"":false,""isPatrolling"":false},
{
            ""NPCName"":""Bob"",""groupOfActivitiesName"":""GuardBossKey"",
""activityPos"":0,""actionPos"":30,""actionInfo"":{
                ""type"":13,""endTime"":
{
                    ""Hour"":6,""Minute"":46,""Day"":1,""Week"":0,""Year"":0,""MonthOfYear"":0,
""WeekOfYear"":0,""DayOfWeek"":1,""DayOfMonth"":1,""DayOfYear"":1},""data"":null},
""isGuard"":true,""isPatrolling"":true},{
            ""NPCName"":""Maxxy JJ"",
""groupOfActivitiesName"":""Max's Night Activities"",""activityPos"":9,
""actionPos"":1,""actionInfo"":{
                ""type"":13,""endTime"":
{
                    ""Hour"":7,""Minute"":11,""Day"":1,""Week"":0,""Year"":0,""MonthOfYear"":0,
""WeekOfYear"":0,""DayOfWeek"":1,""DayOfMonth"":1,""DayOfYear"":1},
""data"":null},""isGuard"":false,""isPatrolling"":false},
{
            ""NPCName"":""Manager George"",""groupOfActivitiesName"":
""George's Night Activities"",""activityPos"":7,""actionPos"":5,""actionInfo"":
{
                ""type"":13,""endTime"":{
                    ""Hour"":6,""Minute"":45,""Day"":1,
""Week"":0,""Year"":0,""MonthOfYear"":0,""WeekOfYear"":0,""DayOfWeek"":1,
""DayOfMonth"":1,""DayOfYear"":1},""data"":null},""isGuard"":false,
""isPatrolling"":false},{
            ""NPCName"":""Slinky"",""groupOfActivitiesName"":
""Patrolling the player house"",""activityPos"":0,""actionPos"":8,""actionInfo"":
{ ""type"":15,""endTime"":null,""data"":null},""isGuard"":true,
""isPatrolling"":true},{
            ""NPCName"":""Karon"",""groupOfActivitiesName"":
""Escort NPC Female"",""activityPos"":0,""actionPos"":3,""actionInfo"":
{ ""type"":6,""endTime"":null,""data"":""Airy""},""isGuard"":true,
""isPatrolling"":false},{
            ""NPCName"":""Sarah"",""groupOfActivitiesName"":
""Sarah's Daytime1 Activities"",""activityPos"":4,""actionPos"":16,
""actionInfo"":{
                ""type"":13,""endTime"":{
                    ""Hour"":6,""Minute"":53,""Day"":1,
""Week"":0,""Year"":0,""MonthOfYear"":0,""WeekOfYear"":0,""DayOfWeek"":1,
""DayOfMonth"":1,""DayOfYear"":1},""data"":null},""isGuard"":false,
""isPatrolling"":false},{
            ""NPCName"":""Donald"",""groupOfActivitiesName"":
""Mike's Night Activities"",""activityPos"":10,""actionPos"":1,""actionInfo"":
{
                ""type"":13,""endTime"":{
                    ""Hour"":7,""Minute"":8,""Day"":1,""Week"":0,
""Year"":0,""MonthOfYear"":0,""WeekOfYear"":0,""DayOfWeek"":1,""DayOfMonth"":1,
""DayOfYear"":1},""data"":null},""isGuard"":false,""isPatrolling"":false},
{
            ""NPCName"":""Daniel"",""groupOfActivitiesName"":""Daniel's Night Activities"",
""activityPos"":9,""actionPos"":1,""actionInfo"":{
                ""type"":13,""endTime"":
{
                    ""Hour"":7,""Minute"":26,""Day"":1,""Week"":0,""Year"":0,""MonthOfYear"":0,
""WeekOfYear"":0,""DayOfWeek"":1,""DayOfMonth"":1,""DayOfYear"":1},""data"":null},
""isGuard"":false,""isPatrolling"":false},{
            ""NPCName"":""Dum"",
""groupOfActivitiesName"":""Dum's Daytime Activities"",""activityPos"":0,
""actionPos"":3,""actionInfo"":{ ""type"":15,""endTime"":null,""data"":null},
""isGuard"":true,""isPatrolling"":true},{
            ""NPCName"":""Pear"",
""groupOfActivitiesName"":""Pear's Daytime Activities"",""activityPos"":0,
""actionPos"":19,""actionInfo"":{ ""type"":15,""endTime"":null,""data"":null},
""isGuard"":true,""isPatrolling"":true},{
            ""NPCName"":""Airy"",
""groupOfActivitiesName"":""Airy's Night Activities"",""activityPos"":2,
""actionPos"":27,""actionInfo"":{ ""type"":15,""endTime"":null,""data"":null},
""isGuard"":false,""isPatrolling"":false},{
            ""NPCName"":""Onna"",
""groupOfActivitiesName"":""Onna's Night Activities"",""activityPos"":15,
""actionPos"":1,""actionInfo"":{
                ""type"":13,""endTime"":{
                    ""Hour"":6,""Minute"":58,
""Day"":1,""Week"":0,""Year"":0,""MonthOfYear"":0,""WeekOfYear"":0,
""DayOfWeek"":1,""DayOfMonth"":1,""DayOfYear"":1},""data"":null},
""isGuard"":false,""isPatrolling"":false},{
            ""NPCName"":""Frank"",
""groupOfActivitiesName"":""Patrol the sidewalk"",""activityPos"":0,
""actionPos"":19,""actionInfo"":{ ""type"":15,""endTime"":null,""data"":null},
""isGuard"":true,""isPatrolling"":true},{
            ""NPCName"":""Tiny"",
""groupOfActivitiesName"":""Tiny's Night Activities"",""activityPos"":7,
""actionPos"":1,""actionInfo"":{
                ""type"":13,""endTime"":{
                    ""Hour"":7,
""Minute"":27,""Day"":1,""Week"":0,""Year"":0,""MonthOfYear"":0,""WeekOfYear"":0,
""DayOfWeek"":1,""DayOfMonth"":1,""DayOfYear"":1},""data"":null},
""isGuard"":true,""isPatrolling"":false},{
            ""NPCName"":""Bethany"",
""groupOfActivitiesName"":""Bethany's Night Activities"",""activityPos"":13,
""actionPos"":1,""actionInfo"":{
                ""type"":13,""endTime"":{
                    ""Hour"":7,
""Minute"":20,""Day"":1,""Week"":0,""Year"":0,""MonthOfYear"":0,
""WeekOfYear"":0,""DayOfWeek"":1,""DayOfMonth"":1,""DayOfYear"":1},
""data"":null},""isGuard"":false,""isPatrolling"":false},
{
            ""NPCName"":""Liberty"",""groupOfActivitiesName"":
""Liberty's Daytime Activities"",""activityPos"":10,""actionPos"":0,
""actionInfo"":{ ""type"":15,""endTime"":null,""data"":null},
""isGuard"":false,""isPatrolling"":false},{
            ""NPCName"":""Jonathan"",
""groupOfActivitiesName"":""Jon's Night Activities"",""activityPos"":10,
""actionPos"":7,""actionInfo"":{
                ""type"":13,""endTime"":{
                    ""Hour"":6,
""Minute"":53,""Day"":1,""Week"":0,""Year"":0,""MonthOfYear"":0,
""WeekOfYear"":0,""DayOfWeek"":1,""DayOfMonth"":1,""DayOfYear"":1},
""data"":null},""isGuard"":false,""isPatrolling"":false},
{
            ""NPCName"":""Karen"",""groupOfActivitiesName"":""Karen's Night Activities"",
""activityPos"":6,""actionPos"":5,""actionInfo"":{
                ""type"":13,""endTime"":
{
                    ""Hour"":6,""Minute"":50,""Day"":1,""Week"":0,""Year"":0,""MonthOfYear"":0,
""WeekOfYear"":0,""DayOfWeek"":1,""DayOfMonth"":1,""DayOfYear"":1},""data"":null},
""isGuard"":false,""isPatrolling"":false},{
            ""NPCName"":""Deaf Jacob"",
""groupOfActivitiesName"":""Daniel's Night Activities"",""activityPos"":3,
""actionPos"":1,""actionInfo"":{
                ""type"":13,""endTime"":{
                    ""Hour"":6,""Minute"":57,
""Day"":1,""Week"":0,""Year"":0,""MonthOfYear"":0,""WeekOfYear"":0,
""DayOfWeek"":1,""DayOfMonth"":1,""DayOfYear"":1},""data"":null},
""isGuard"":false,""isPatrolling"":false}],""PositionSettings"":
{
            ""positionSettingsFloat"":[16.4467888,25.4782162,0.0,6.54706573,-1.96686554,0.0,
7.44615936,-24.5106964,0.0,15.9387894,83.4930344,0.0,-19.95039,-68.72443,0.0,
-2.72161865,-24.23288,0.0,44.8036842,-89.8221741,0.0,-22.576416,-88.41278,0.0,
15.4415359,-49.5058746,0.0,16.439064,-49.5045929,0.0,31.0020638,-97.2513,0.0,
15.9443436,-24.5131073,0.0,-34.75289,-94.54274,0.0,1.35977459,10.2508888,0.0,
-32.7499428,75.29346,0.0,-55.02148,-101.001556,0.0,-50.4470024,24.5154781,0.0,
-45.9396439,-0.51335907,0.0,-16.8447952,-70.2588654,0.0,-22.5303154,-53.97948,
0.0,-32.74996,75.8934555,0.0,-50.4521179,53.5282936,0.0,-20.3977013,-55.9445839,
0.0,13.5150747,-88.55731,0.0,-49.94268,-25.0213776,0.0,-53.2875061,-90.3912659,
0.0,-50.21492,-49.5000153,0.0,-21.2568359,-86.0591,0.0,-77.444664,90.9747,0.0],
""positionNames"":[""Mike"",""Helpful Phil"",""Sassy Sam"",""Jade"",""Dee"",
""Joe(Unknown)"",""Slowpoke"",""Jessie"",""Isaac"",""Autumn"",""Bob"",
""Maxxy JJ"",""Manager George"",""Slinky"",""Karon"",""Sarah"",""Donald"",""Daniel"",
""Dum"",""Pear"",""Airy"",""Onna"",""Frank"",""Tiny"",""Bethany"",
""Liberty"",""Jonathan"",""Karen"",""Deaf Jacob""],""playerPositionSettingsFloat"":
[14.2435446,-2.58444166,0.0]},""LightSettings"":[false,false,false,false,false,false,false,true,
false,false,false,false,false,false,true,false,false,false,false,false,true,true,
false,false,false,false,true,false,false,false,false,false,false,true,false,
false,false,false,false,false,false,false,false,false,false,true,false,true,
false,false,false,false,false,false,false,true,false],""PaperSettings"":null,
""PenSettings"":null,""KeySettings"":null,""BookSettings"":null,
""MiscSettings"":null,""PensHeld"":0,""PaperHeld"":0,""BooksHeld"":0,
""LettersHeld"":null,""Logs"":null,""LogsCounter"":0,""PeopleKnown"":null}";


    saveFile = JsonConvert.DeserializeObject<SaveFile>(nextDayData);
        

    }

    public void ExportSaveData(string savename)
    {
        Debug.Log("Testing File saving");

        // save next day data


        string savesFolderPath = Path.Combine(pathString, "saves");
        string totalPathString = System.IO.Path.Combine(savesFolderPath, savename + ".json");

        Debug.Log("Exporting data from folder " + savesFolderPath);
        Debug.Log("Exporting data from file path" + totalPathString);

        string saveData = JsonConvert.SerializeObject(saveFile); // converts data in the NPCsettings object into a string

        System.IO.Directory.CreateDirectory(savesFolderPath);

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

        Debug.Log("Importing data from folder " + savesFolder);
        Debug.Log("Importing data from file " + saveFile);
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
        objectSettings.GetAllObjectSettings(bookList);
        saveFile.BookSettings = objectSettings.objectSettings;

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
        objectSettings.objectSettings = saveFile.BookSettings;
        objectSettings.SetAllObjectSettings(bookList);
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
        saveFile.BooksHeld = player.invScript.Books;
        saveFile.LettersHeld = player.invScript.Letters.ToArray();
    }

    public void SetInventory()
    {
        player.invScript.Pens = saveFile.PensHeld;
        player.invScript.Paper = saveFile.PaperHeld;
        player.invScript.Books = saveFile.BooksHeld;
        player.invScript.inventoryObjects = new List<GameObject>();
        player.invScript.inventoryNames = new List<string>();
        for(int i = 0; i < keyList.Length; i++)
        {
            if(!saveFile.KeySettings[i])
            {
                player.invScript.inventoryNames.Add(keyList[i].GetComponent<KeyScript>().objectName);
                player.invScript.inventoryObjects.Add(keyList[i]);
                player.invScript.inventoryDict.Add(keyList[i].GetComponent<KeyScript>().objectName, keyList[i]);
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
        player.ReduceSuspicion(2 * (Clock.timeToNextDay().Hour * 60 + Clock.timeToNextDay().Minute)); // reduce suspicion
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

        if (time.Hour == 21 && time.Minute == 0)
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
