using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PositionSet : ScriptableObject
{
    // keep each groupOfActivitiesName in list unique
    public string NPCName;  // the name of the NPC
    public double xPos; // the position of the current activity in the group of activities list
    public double yPos; // the position of the current action in the activities list

}
