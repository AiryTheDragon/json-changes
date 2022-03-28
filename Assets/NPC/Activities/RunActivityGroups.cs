using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunActivityGroups
{
    public List<GroupOfActivities> ActivityGroups;

    public int ActivityGroupIndex;
    public GroupOfActivities RunningGroup;

    public int ActivityIndex;
    public Activity RunningActivity;

    public int ActionIndex;
    public ActivityAction RunningAction;


    public RunActivityGroups(List<GroupOfActivities> activityGroups)
    {
        this.ActivityGroups = activityGroups;
    }

    public void RunNextActivityGroup(ClockTime time)
    {
        ActivityGroupIndex = -1;
        RunningGroup = null;

        for(int i = 0; i < ActivityGroups.Count; i++)
        {
            if(ActivityGroups[i].TimeWithin(time))
            {
                ActivityGroupIndex = i;
                ActivityIndex = 0;
                ActionIndex = 0;
                RunningGroup = ActivityGroups[i];
                RunningActivity = RunningGroup.Activities[0];
                RunningAction = RunningActivity.actions[0];
                break;
            }
        }
    }

    public void RunActivityGroup(GroupOfActivities activityGroup)
    {
        RunningGroup = activityGroup;
        ActionIndex = 0;
        ActivityIndex = 0;
        RunningActivity = activityGroup.Activities[0];
        RunningAction = RunningActivity.actions[0];
    }

    public ActivityAction GetCurrentAction()
    {
        if(RunningGroup == null)
        {
            return null;
        }
        return RunningAction;
    }

    public void CompleteAction(ClockTime time)
    {
        if(!RunningGroup.TimeWithin(time))
        {
            RunNextActivityGroup(time);
            return;
        }
        ActionIndex++;
        if(ActionIndex >= RunningActivity.actions.Count)
        {
            ActionIndex = 0;
            ActivityIndex++;
            if(ActivityIndex > RunningGroup.Activities.Count)
            {
                ActivityIndex = 0;
            }
            RunningActivity = RunningGroup.Activities[ActivityIndex];
            RunningAction = RunningActivity.actions[ActionIndex];
        }
        else
        {
            RunningAction = RunningActivity.actions[ActionIndex];
        }
    }

/*
    public bool IsActivityComplete()
    {
        return currentAction >= RunningActivity.actions.Count;
    }*/
}
