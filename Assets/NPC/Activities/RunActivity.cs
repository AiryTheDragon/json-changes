using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunActivity
{
    private int currentAction;

    public Activity activity;


    public RunActivity(Activity activity)
    {
        this.activity = activity;
    }

    public void ResetActivity()
    {
        currentAction = 0;
    }

    public ActivityAction GetCurrentAction()
    {
        if(currentAction >= activity.actions.Count)
        {
            return null;
        }
        var my = activity.actions[currentAction];
        return my;
    }

    public void CompleteAction()
    {
        currentAction++;
    }

    public bool IsComplete()
    {
        return currentAction >= activity.actions.Count;
    }
}
