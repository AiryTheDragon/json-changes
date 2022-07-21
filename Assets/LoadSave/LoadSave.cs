using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject[] miscList;
    public PickableObjectSettings paperSettings;
    public PickableObjectSettings penSettings;
    public PickableObjectSettings keySettings;
    public PickableObjectSettings miscSettings;
    public ClockBehavior Clock;
    public Player player;

    public void Start()
    {
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
            Debug.Log("Getting behaviors, positions, light and object settings.");
            CreateSaveData();
        }

        
        /*
        if (time.Hour==6 && time.Minute==0)
        {
            Debug.Log("Setting behaviors.");
            LoadSaveData();
        }
        */
        
    }
}
