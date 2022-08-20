/*
 * This class stores information regarding NPC behavior and is used in saving and restoring NPC information
 * Created by Michael Clinesmith
 */

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
    public bool isGuard; // flag if the NPC is a guard
    public bool isPatrolling; // flag if the NPC guard is patrolling

}
