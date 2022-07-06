using System.Collections;
using System.Collections.Generic;
using UnityEngine;


   
public class PositionSettings : ScriptableObject
{

    public Vector3[] positionSettings;
    public string[] positionNames;
    public Vector3 playerPositionSettings;

    public void Start()
    {

    }

    public Vector3[] getAllPositionSettings(NPCBehavior[] NPClist)
    {
        positionSettings = new Vector3[NPClist.Length];
        positionNames = new string[NPClist.Length];

        for (int i=0; i<NPClist.Length; i++)
        {
            positionSettings[i] = NPClist[i].transform.position;
            positionNames[i] = NPClist[i].Name;
        }

        return positionSettings;
    }

    public void setAllPositionSettings(NPCBehavior[] NPClist)
    {

        for (int i = 0; i < NPClist.Length; i++)
        {
            int j = getListPos(NPClist[i].Name);
            if (j >= 0)
            {
                NPClist[i].transform.position = positionSettings[j];
            }
            else
            {
                Debug.Log("Position settings for " + NPClist[i].Name + " not found.");
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
        for (int i=0; i< positionSettings.Length && !found; i++)
        {
            if (positionNames[i].Equals(name))
            {
                pos = i;
                found = true;
            }
        }
        return pos;
    }



}
