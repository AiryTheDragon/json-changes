using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityWalk : ActivityAction
{
    public GameObject Destination;

    void Start()
    {
        Destination.SetActive(false);
    }
}
