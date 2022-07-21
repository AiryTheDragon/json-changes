using System.Collections;
using System.Collections.Generic;
using UnityEngine;

   
public class LightSettings : ScriptableObject
{

    public bool[] lightSettings;

    public void Start()
    {

    }

    public bool[] getAllLightSettings(LampBehavior[] lampList)
    {
        lightSettings = new bool[lampList.Length];

        for (int i=0; i< lampList.Length; i++)
        {
            lightSettings[i] = lampList[i].on;
        }

        return lightSettings;
    }

    public void setAllLightSettings(LampBehavior[] lampList)
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
