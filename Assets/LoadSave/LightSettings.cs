using System.Collections;
using System.Collections.Generic;
using UnityEngine;

   
public class LightSettings
{

    public bool[] lightSettings;

    public bool[] GetAllLightSettings(LampBehavior[] lampList)
    {
        lightSettings = new bool[lampList.Length];

        for (int i=0; i< lampList.Length; i++)
        {
            lightSettings[i] = lampList[i].on;
        }

        return lightSettings;
    }

    public void SetAllLightSettings(LampBehavior[] lampList)
    {

        for (int i = 0; i < lampList.Length && i<lightSettings.Length; i++)
        {   
            if (lightSettings[i])
            {
                lampList[i].TurnOnQuiet();
            }
            else
            {
                lampList[i].TurnOffQuiet();
            }         
        }
        return;
    }

}
