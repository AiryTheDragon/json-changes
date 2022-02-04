using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NCPBehavior : MonoBehaviour
{
    public List<GameObject> path;

    public List<Activity> Activities;

    private DateTime LastPathfind = DateTime.Now;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Activities[0].IsRunning)
        {
            var activity = Activities[0];
            if(activity.GetDestination() != null)
            {
                if(DateTime.Now - LastPathfind > TimeSpan.FromMilliseconds(500))
                {
                    // TODO: pathfind
                }
            }
        }
    }
}
