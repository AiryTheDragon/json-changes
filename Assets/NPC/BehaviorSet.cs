using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BehaviorSet : ScriptableObject
{
    // keep each groupOfActivitiesName in list unique
    public string NPCName;  // the name of the NPC
    public string groupOfActivitiesName; // the name of the active group of activities
    public int activityPos; // the position of the current activity in the group of activities list
    public int actionPos; // the position of the current action in the activities list
    public ActivityActionSet actionInfo; // object with info on the current action

}
