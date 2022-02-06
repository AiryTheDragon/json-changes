using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardConfigurer : MonoBehaviour
{
    public int SuspicionPerMinute = 0;

    public bool EscortOnSight;

    public Activity EscortPlayerActivity;

    public Activity EscortNPCActivity;

    public Activity PatrolActivity;

    public Transform EscortTarget;
}
