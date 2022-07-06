using System.Collections;
using System.Collections.Generic;
using UnityEngine;


   
public class LoadSave : MonoBehaviour
{

    public NPCBehavior[] npcList;

    public void Start()
    {
        npcList = GameObject.FindObjectsOfType<NPCBehavior>();
    }

}
