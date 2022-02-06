using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activity : MonoBehaviour
{
    public List<ActivityAction> actions;

    public ActivityFrequencies ActivityFrequency;

    public ClockTime ActivityTime;

    public string Name;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsActivityTime(ClockTime time)
    {
        switch(ActivityFrequency)
        {
            case Activity.ActivityFrequencies.Daily:
                if(ActivityTime.Minute == time.Minute && ActivityTime.Day == time.Day)
                {
                    return true;
                }
                break;
            case Activity.ActivityFrequencies.Hourly:
                if(ActivityTime.Minute == time.Minute)
                {
                    return true;
                }
                break;
            case Activity.ActivityFrequencies.Once:
                if(ActivityTime.Minute == time.Minute && ActivityTime.Day == time.Day && ActivityTime.Hour == time.Hour)
                {
                    return true;
                }
                break;
            case Activity.ActivityFrequencies.Weekly:
                if(ActivityTime.Day == time.DayOfWeek && ActivityTime.Hour == time.Hour && ActivityTime.Minute == time.Minute)
                {
                    return true;
                }
                break;
            case Activity.ActivityFrequencies.Monthly:
                if(ActivityTime.Day == time.DayOfMonth && ActivityTime.Minute == time.Minute && ActivityTime.Hour == time.Hour)
                {
                    return true;
                }
                break;
            case Activity.ActivityFrequencies.Yearly:
                if(ActivityTime.Day == time.DayOfYear && ActivityTime.Hour == time.Hour && ActivityTime.Minute == time.Minute)
                {
                    return true;
                }
                break;
        }
        return false;
    }

    public enum ActivityFrequencies
    {
        Hourly,
        Daily,
        Once,
        Weekly,
        Monthly,
        Yearly
    }
}
