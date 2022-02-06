using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannedActivitiesBehavior : MonoBehaviour
{
    public List<Activity> BannedActivities;

    public List<int> BanLevels;

    public void BanActivity(Activity activity)
    {
        if(BannedActivities.Contains(activity))
        {
            int i = BannedActivities.IndexOf(activity);
            BanLevels[i] += 1;
        }
        else
        {
            BannedActivities.Add(activity);
            BanLevels.Add(1);
        }
    }

    public int GetBanLevel(Activity activity)
    {
        if(BannedActivities.Contains(activity))
        {
            return BanLevels[BannedActivities.IndexOf(activity)];
        }
        else
        {
            return 0;
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
