using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BannableActivityList : MonoBehaviour
{
    public List<ActivityCategory> ActivityCategories;

#if DEVELOPMENT_BUILD

    public void Start()
    {
        /*foreach (var activity in ActivityCategories)
        {
            var foundList = ActivityCategories.Except(new List<ActivityCategory>() {activity})
                .Where(x => x.Activities.Except(activity.Activities).Count() != x.Activities.Count).ToList();
            if(foundList.Any())
            {
                throw new System.Exception($"Activities in group {activity.GroupName} are not in only one group.");
            }
        }*/

        var activities = GameObject.FindObjectsOfType<Activity>().ToList();
        foreach(var activity in activities)
        {
            var foundList = activities.Except(new List<Activity>() {activity})
                .Where(x => x.Name == activity.Name && x.name == activity.name).ToList();
            if(foundList.Any())
            {
                throw new System.Exception($"Activity {activity.Name} ({activity.name}) has a duplicate.");
            }
        }
    }

#endif

}
