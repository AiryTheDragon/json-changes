using System.Collections;
using System.Collections.Generic;
using UnityEngine;

   
public class PickableObjectSettings
{

    public bool[] objectSettings;

    public bool[] GetAllObjectSettings(GameObject[] objectList)
    {
        objectSettings = new bool[objectList.Length];

        for (int i=0; i< objectList.Length; i++)
        {
            objectSettings[i] = objectList[i].activeSelf;
        }

        return objectSettings;
    }

    public void SetAllObjectSettings(GameObject[] objectList)
    {

        for (int i = 0; i < objectList.Length && i< objectSettings.Length; i++)
        {
            objectList[i].SetActive(objectSettings[i]);           
        }
        return;
    }

}
