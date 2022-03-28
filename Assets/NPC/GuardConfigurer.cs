using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardConfigurer : MonoBehaviour
{
    public int SuspicionPerMinute = 0;

    public bool EscortOnSight;

    public GroupOfActivities EscortPlayerActivityGroup;

    public GroupOfActivities EscortNPCActivityGroup;

    public GroupOfActivities PatrolActivityGroup;

    public Transform EscortTarget;
}
