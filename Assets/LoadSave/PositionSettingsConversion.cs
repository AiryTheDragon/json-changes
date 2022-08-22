using System.Collections;
using System.Collections.Generic;
using UnityEngine;


   
public class PositionSettingsConversion
{

    public float[] positionSettingsFloat;
    public string[] positionNames;
    public float[] playerPositionSettingsFloat;

    public void Start()
    {

    }

    public void setPositions(Vector3[] posSettings, string[] posNames, Vector3 playerPos)
    {
        positionSettingsFloat = new float[posSettings.Length * 3];
        positionNames = new string[posNames.Length];
        playerPositionSettingsFloat = new float[3];
        playerPositionSettingsFloat[0] = playerPos.x;
        playerPositionSettingsFloat[1] = playerPos.y;
        playerPositionSettingsFloat[2] = playerPos.z;
        
        for (int i=0; i<posSettings.Length; i++)
        {
            positionSettingsFloat[3 * i] = posSettings[i].x;
            positionSettingsFloat[3 * i + 1] = posSettings[i].y;
            positionSettingsFloat[3 * i + 2] = posSettings[i].z;
            positionNames[i] = posNames[i];
        }

    }

    public PositionSettings convertToVector3()
    {
        PositionSettings conversion = new PositionSettings();
        conversion.SetPositions(positionSettingsFloat, positionNames, playerPositionSettingsFloat);

        return conversion;
    }

}
