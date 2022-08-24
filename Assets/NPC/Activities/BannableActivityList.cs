using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BannableActivityList : MonoBehaviour
{
    public List<ActivityCategory> ActivityCategories;

    public void Start()
    {
        foreach (var activity in ActivityCategories)
        {
            var foundList = ActivityCategories.Except(new List<ActivityCategory>() {activity})
                .Where(x => x.Activities.Except(activity.Activities).Count() != x.Activities.Count).ToList();
            if(foundList.Any())
            {
                throw new System.Exception($"Activities in group {activity.GroupName} are not in only one group.");
            }
        }
    }
}
