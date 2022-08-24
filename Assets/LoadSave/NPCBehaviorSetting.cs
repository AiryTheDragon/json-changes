using System.Collections;
using System.Collections.Generic;
using UnityEngine;


   
public class NPCBehaviorSetting
{

    public BehaviorSet[] behaviorSetting;

    public BehaviorSet[] getAllBehaviorSettings(NPCBehavior[] NPClist)
    {
        behaviorSetting = new BehaviorSet[NPClist.Length];

        for (int i=0; i<NPClist.Length; i++)
        {
            behaviorSetting[i] = NPClist[i].getBehavior();
        }

        return behaviorSetting;
    }

    public void setAllBehaviorSettings(NPCBehavior[] NPClist)
    {
        if(behaviorSetting is null)
        {
            throw new System.Exception("BehaviorSetting has not been loaded.");
        }
        Debug.Log("behaviorSetting length" + behaviorSetting.Length);
        for (int i = 0; i < NPClist.Length; i++)
        {



            int j = getListPos(NPClist[i].Name);
            if (j >= 0)
            {
                /*
                Debug.Log("Setting behaviors for " + NPClist[i] + " into position " + i + " from position " + j + ".");
                Debug.Log("Behavior setting[" + j + "].groupOfActivitiesName: " + behaviorSetting[j].groupOfActivitiesName);
                Debug.Log("Behavior setting[" + j + "].activityPos: " + behaviorSetting[j].activityPos);
                Debug.Log("Behavior setting[" + j + "].actionPos: " + behaviorSetting[j].actionPos);
                Debug.Log("Behavior setting[" + j + "].actionInfo.type: " + behaviorSetting[j].actionInfo.type.ToString());
                Debug.Log("Behavior setting[" + j + "].isGuard: " + behaviorSetting[j].isGuard);
                Debug.Log("Behavior setting[" + j + "].isPatrolling: " + behaviorSetting[j].isPatrolling);
                */

                NPClist[i].setBehavior(behaviorSetting[j]);
            }
            else
            {
                Debug.Log("Behavior settings for " + NPClist[i].Name + " not found.");
            }
        }

        return;
    }

    // method to get the position of the NPC in the Behavior list
    // returns the index if it exists, otherwise returns -1
    private int getListPos(string name)
    {
        int pos = -1;
        bool found = false;

        for (int i=0; i<behaviorSetting.Length && !found; i++)
        {
            if (behaviorSetting[i].NPCName.Equals(name))
            {
                pos = i;
                found = true;
            }
        }
        return pos;
    }



}
