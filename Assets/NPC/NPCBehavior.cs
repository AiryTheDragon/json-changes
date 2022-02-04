using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;
public class NPCBehavior : AIPath, INeedsClockUpdate
{
    //public List<GameObject> paths;

    public List<Activity> Activities;

    private DateTime LastPathfind = DateTime.Now;

    private RunActivity runningActivity;

    private ClockTime waitUntil;

    // Start is called before the first frame update
    protected override void Start()
    {
        RunActivity(Activities[0]);
        //GetComponent<AIDestinationSetter>().target = runningActivity.GetDestination().GetComponent<Transform>();
        //base.Start();
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public void RunActivity(Activity activity)
    {
        runningActivity = new RunActivity(activity);
        BeginAction(runningActivity.GetCurrentAction());
    }

    public void BeginAction(ActivityAction action)
    {
        if(action is null)
        {
            return;
        }
        if(action is ActivityWait)
        {
            waitUntil = new ClockTime(ClockBehavior.MainClockBehavior.Time);
            waitUntil.AddMinutes(((ActivityWait)action).Minutes);
            ClockBehavior.NeedsClockUpdate.Add(this);
        }
        else if (action is ActivityRepeat)
        {
            runningActivity.ResetActivity();
            BeginAction(runningActivity.GetCurrentAction());
        }
        else if (action is ActivityWalk)
        {
            var walk = (ActivityWalk)action;
            GetComponent<AIDestinationSetter>().target = walk.Destination.GetComponent<Transform>();
        }
        else if (action is ActivityWaitUntilTime)
        {
            waitUntil = new ClockTime(((ActivityWaitUntilTime)action).Time);
            waitUntil.Day = ClockBehavior.MainClockBehavior.Time.Day;
            ClockBehavior.NeedsClockUpdate.Add(this);
        }
    }


    public override void OnTargetReached()
    {
        /*runningActivity.DestinationReached();
        if(runningActivity.GetDestination() != null)
        {
            GetComponent<AIDestinationSetter>().target = runningActivity.GetDestination().GetComponent<Transform>();
        }*/
        if(runningActivity.GetCurrentAction() is ActivityWalk)
        {
            runningActivity.CompleteAction();
            BeginAction(runningActivity.GetCurrentAction());
        }
        base.OnTargetReached();
    }

    public void UpdateClock(ClockTime time)
    {
        if(runningActivity.GetCurrentAction() is ActivityWait)
        {
            if(waitUntil.Day < time.Day ||
                waitUntil.Day == time.Day && waitUntil.Hour < time.Hour ||
                waitUntil.Day == time.Day && waitUntil.Hour == time.Hour && waitUntil.Minute <= time.Minute)
            {
                runningActivity.CompleteAction();
                ClockBehavior.NeedsClockUpdate.Remove(this);
                BeginAction(runningActivity.GetCurrentAction());
            }
        }
        else if (runningActivity.GetCurrentAction() is ActivityWaitUntilTime)
        {
            if(waitUntil.Day < time.Day ||
                waitUntil.Day == time.Day && waitUntil.Hour < time.Hour ||
                waitUntil.Day == time.Day && waitUntil.Hour == time.Hour && waitUntil.Minute <= time.Minute)
            {
                runningActivity.CompleteAction();
                ClockBehavior.NeedsClockUpdate.Remove(this);
                BeginAction(runningActivity.GetCurrentAction());
            }
        }
    }
}
