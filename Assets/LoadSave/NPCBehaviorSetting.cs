using System.Collections;
using System.Collections.Generic;
using UnityEngine;


   
public class NPCBehaviorSetting : ScriptableObject
{

    public BehaviorSet[] behaviorSetting;
    public string[] names

    public void Start()
    {
        npcList = GameObject.FindObjectsOfType<NPCBehavior>();
    }

}
