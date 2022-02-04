using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activity : MonoBehaviour
{
    public List<GameObject> path;

    private GameObject currentDestination;

    public ActivityTypes ActivityType;

    public ClockTime ActivityTime;

    public bool IsRunning { get; set; }

    public bool LoopActivity { get; set; }

    public int RunTimes { get; private set; }

    public string Name { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginActivity()
    {
        IsRunning = true;
        RunTimes = 0;
    }

    public virtual GameObject GetDestination()
    {
        if(currentDestination != null)
        {
            return currentDestination;
        }
        else
        {
            if(RunTimes == 0)
            {
                currentDestination = path[0];
                return currentDestination;
            }
            else if (RunTimes > 0 && LoopActivity)
            {
                currentDestination = path[0];
                return currentDestination;
            }
            else
            {
                return null;
            }
        }
    }

    public void DestinationReached()
    {
        int i;
        for(i = 0; i < path.Count; i++)
        {
            if(path[i] == currentDestination)
            {
                break;
            }
        }
        if(path.Count > i)
        {
            currentDestination = path[i + 1];
        }
        else
        {
            currentDestination = null;
            RunTimes++;
        }
    }

    public bool IsActivityTime(ClockTime time)
    {
        switch(ActivityType)
        {
            case Activity.ActivityTypes.Daily:
                if(ActivityTime.Minute == time.Minute && ActivityTime.Day == time.Day)
                {
                    return true;
                }
                break;
            case Activity.ActivityTypes.Hourly:
                if(ActivityTime.Minute == time.Minute)
                {
                    return true;
                }
                break;
            case Activity.ActivityTypes.Once:
                if(ActivityTime.Minute == time.Minute && ActivityTime.Day == time.Day && ActivityTime.Hour == time.Hour)
                {
                    return true;
                }
                break;
            case Activity.ActivityTypes.Weekly:
                if(ActivityTime.Day == time.DayOfWeek && ActivityTime.Hour == time.Hour && ActivityTime.Minute == time.Minute)
                {
                    return true;
                }
                break;
            case Activity.ActivityTypes.Monthly:
                if(ActivityTime.Day == time.DayOfMonth && ActivityTime.Minute == time.Minute && ActivityTime.Hour == time.Hour)
                {
                    return true;
                }
                break;
            case Activity.ActivityTypes.Yearly:
                if(ActivityTime.Day == time.DayOfYear && ActivityTime.Hour == time.Hour && ActivityTime.Minute == time.Minute)
                {
                    return true;
                }
                break;
        }
        return false;
    }

    public enum ActivityTypes
    {
        Hourly,
        Daily,
        Once,
        Weekly,
        Monthly,
        Yearly
    }
}
