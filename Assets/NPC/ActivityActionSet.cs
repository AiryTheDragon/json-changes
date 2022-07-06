using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ActivityActionSet : ScriptableObject
{
    // keep each groupOfActivitiesName in list unique

    public ActivityActionType type;
    public ClockTime endTime;
    public string data;

}
