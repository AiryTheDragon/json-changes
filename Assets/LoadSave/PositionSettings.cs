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

    public Vector3 getPlayerPositionSettings(Player player)
    {
        playerPositionSettings = player.transform.position;
        return player.transform.position;
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

    public void setPlayerPositionSettings(Player player)
    {
        player.transform.position = playerPositionSettings;
        return;
    }

    public void setPositions(float[] posSettings, string[] posNames, float[] playerPos)
    {
        positionSettings = new Vector3[posSettings.Length / 3];
        positionNames = new string[posNames.Length];
        playerPositionSettings = new Vector3(playerPos[0], playerPos[1], playerPos[2]);

        for (int i = 0; i < posNames.Length; i++)
        {
            positionSettings[i] = new Vector3(posSettings[3 * i], posSettings[3 * i + 1], posSettings[3 * i + 2]);
            positionNames[i] = posNames[i];
        }
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

    public PositionSettingsConversion convertToFloats()
    {
        PositionSettingsConversion conversion = PositionSettingsConversion.CreateInstance<PositionSettingsConversion>();
        conversion.setPositions(positionSettings, positionNames, playerPositionSettings);

        return conversion;
    }

}
