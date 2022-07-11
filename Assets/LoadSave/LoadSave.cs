using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSave : MonoBehaviour, INeedsClockUpdate
{

    public NPCBehavior[] npcList;
    public NPCBehaviorSetting NPCsettings;
    public PositionSettings positionSettings;
    public ClockBehavior Clock;
    public Player player;

    public void Start()
    {
        NPCsettings = NPCBehaviorSetting.CreateInstance<NPCBehaviorSetting>();
        positionSettings = PositionSettings.CreateInstance<PositionSettings>();
        npcList = GameObject.FindObjectsOfType<NPCBehavior>();

        Clock.NeedsClockUpdate.Add(this);
    }

    public void CreateSaveData()
    {
        CreateBehaviorSettings();
        CreatePositionSettings();
    }

    public void LoadSaveData()
    {
        SetBehaviorSettings();
        SetPositionSettings();
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

    // currently being used for testing purposes
    public void UpdateClock(ClockTime time)
    {
        if (time.Hour==5 && time.Minute==0)
        {
            Debug.Log("Getting behaviors & positions.");
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
