using System.Collections;
using System.Collections.Generic;
using UnityEngine;


   
public class LoadSave : MonoBehaviour, INeedsClockUpdate
{

    public NPCBehavior[] npcList;
    public NPCBehaviorSetting NPCsettings;
    public ClockBehavior Clock;

    public void Start()
    {
        NPCsettings = new NPCBehaviorSetting();
        npcList = GameObject.FindObjectsOfType<NPCBehavior>();
        Clock.NeedsClockUpdate.Add(this);
    }

    public void CreateBehaviorSettings()
    {
        NPCsettings.getAllBehaviorSettings(npcList);
    }

    public void SetBehaviorSettings()
    {
        NPCsettings.setAllBehaviorSettings(npcList);
    }

    // currently being used for testing purposes
    public void UpdateClock(ClockTime time)
    {
        if (time.Hour==5 && time.Minute==0)
        {
            Debug.Log("Getting behaviors.");
            CreateBehaviorSettings();
        }
    }
}
