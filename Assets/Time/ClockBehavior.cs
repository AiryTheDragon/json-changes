using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ClockBehavior : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D Sun;

    public DateTime LastUpdate = DateTime.Now;

    public ClockTime Time = new ClockTime(0, 6, 50);

    public ClockTime NewDayTime = new ClockTime(0, 6, 50);

    public List<INeedsClockUpdate> NeedsClockUpdate = new List<INeedsClockUpdate>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(DateTime.Now - LastUpdate > TimeSpan.FromSeconds(1))
        {
            LastUpdate = DateTime.Now;
            Time.AddMinutes(1);

            if(Time.Hour < 5)
            {
                Sun.intensity = 0.1f;
            }
            else if(Time.Hour < 6)
            {
                Sun.intensity = 0.1f + 0.4f * Time.Minute / 60;
            }
            else if (Time.Hour < 7)
            {
                Sun.intensity = 0.5f + 0.4f * Time.Minute / 60;
            }
            else if (Time.Hour < 8)
            {
                Sun.intensity = Math.Min(0.9f + 0.4f * Time.Minute / 60, 1.0f);
            }
            else if (Time.Hour < 20)
            {
                Sun.intensity = 1f;
            }
            else if (Time.Hour < 21)
            {
                Sun.intensity = 1f - 0.4f * Time.Minute / 60;
            }
            else if (Time.Hour < 22)
            {
                Sun.intensity = 0.6f - 0.4f * Time.Minute / 60;
            }
            else if (Time.Hour < 23)
            {
                Sun.intensity = Math.Max(0.2f - 0.4f * Time.Minute / 60, 0.1f);
            }
            else {
                Sun.intensity = 0.1f;
            }

            
            for(int i = 0; i < NeedsClockUpdate.Count; i++)
            {
                NeedsClockUpdate[i].UpdateClock(Time);
            }
        }     

    }
    // This method will calculate the time to the next wakeup time
    public ClockTime timeToNextDay()
    {

        ClockTime toNextDay = new ClockTime(NewDayTime);
        toNextDay = NewDayTime.subtractClockTime(Time);
        toNextDay.Day = 0;

        return toNextDay;
    }

    // The method will return the next wakeup time with the proper day
    public ClockTime nextDayTime()
    {
        ClockTime nextTime = new ClockTime(NewDayTime);
        ClockTime thisDayTime = new ClockTime(Time);
        thisDayTime.Day = 0;
        ClockTime difference = NewDayTime.subtractClockTime(thisDayTime);  // if the curent time is after reset time, difference.hour = -1
        nextTime.Day = Time.Day - difference.Day; // add one if current time is after reset time
 //       Debug.Log("Difference between now and next time is " + difference.Day + ", hour " + difference.Hour + ", minute " + difference.Minute);
 //       Debug.Log("Time being set to day " + nextTime.Day + ", hour " + nextTime.Hour + ", minute " + nextTime.Minute);

        return nextTime;
    }
}
