using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupOfActivities : MonoBehaviour
{
    public string GroupName;

    public List<Activity> Activities;

    // TODO: Make this a clocktime.
    public ClockTime startTime;
    public int StartHour;
    public int StartMinute;
    public int EndHour;
    public int EndMinute;
    public bool LoopMidnight;

    public bool TimeWithin(ClockTime time)
    {
        if(!LoopMidnight)
        {
            if(time.Hour > StartHour)
            {
                if ((time.Hour < EndHour) || (time.Hour == EndHour && time.Minute < EndMinute))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (time.Hour == StartHour)
            {
                if(time.Minute >= StartMinute)
                {
                    if ((time.Hour < EndHour) || (time.Hour == EndHour && time.Minute < EndMinute))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            if((time.Hour > StartHour || (time.Hour == StartHour && time.Minute >= StartMinute)) ||
                ((time.Hour < EndHour) || (time.Hour == EndHour && time.Minute < EndMinute)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
