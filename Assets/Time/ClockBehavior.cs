using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ClockBehavior : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D Sun;

    public DateTime LastUpdate = DateTime.Now;

    public ClockTime Time = new ClockTime(0, 4, 0);

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
            for(int i = 0; i < NeedsClockUpdate.Count; i++)
            {
                NeedsClockUpdate[i].UpdateClock(Time);
            }
        }
    }
}
