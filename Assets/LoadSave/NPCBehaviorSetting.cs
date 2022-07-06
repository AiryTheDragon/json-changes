using System.Collections;
using System.Collections.Generic;
using UnityEngine;


   
public class NPCBehaviorSetting : ScriptableObject
{

    public BehaviorSet[] behaviorSetting;

    public void Start()
    {

    }

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

        for (int i = 0; i < NPClist.Length; i++)
        {
            int j = getListPos(NPClist[i].Name);
            if (j >= 0)
            {
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
