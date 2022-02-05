using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ClockBehavior : MonoBehaviour
{
    public static ClockBehavior MainClockBehavior;

    //public static List<Activity> Activities;

    public DateTime LastUpdate = DateTime.Now;

    public ClockTime Time = new ClockTime(0, 8, 0);

    public static List<INeedsClockUpdate> NeedsClockUpdate = new List<INeedsClockUpdate>();

    // Start is called before the first frame update
    void Start()
    {
        MainClockBehavior = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(DateTime.Now - LastUpdate > TimeSpan.FromSeconds(1))
        {
            LastUpdate = DateTime.Now;
            Time.AddMinutes(1);
            /*
            for(int i = 0; i < Activities.Count; i++)
            {
                if(Activities[i].IsActivityTime(Time))
                {
                    Activities[i].BeginActivity();
                }
                if(Activities[i].ActivityType == Activity.ActivityTypes.Once)
                {
                    Activities.RemoveAt(i);
                    i--;
                }
            }
            */
            Debug.Log("Updated Clock");
            for(int i = 0; i < NeedsClockUpdate.Count; i++)
            {
                NeedsClockUpdate[i].UpdateClock(Time);
            }
        }
    }
}
