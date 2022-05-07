using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannedActivitiesBehavior : MonoBehaviour, INeedsClockUpdate
{
    public List<Activity> BannedActivities;

    public List<int> BanLevels;

    public BannableActivityList ListOfBannableActivities;

    private ClockBehavior Clock;

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
        Clock = GameObject.Find("Clock").GetComponent<ClockBehavior>();
        Clock.NeedsClockUpdate.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateClock(ClockTime time)
    {
        // time to ban and unban activities (at 7:00 every day)
        if (time.Minute==0 && time.Hour==7)
        {
            UpdateBans();

        }
    }
    private void UpdateBans()
    {
        // algorithm to ban and unban activities.  Choose one sleep to ban and one to unban
        int BanValue1 = -1;
        int BanValue2 = -1;

        int UnBanValue = -1;
        ActivityCategory ActivityBanned1 = null;
        ActivityCategory ActivityBanned2 = null;
        ActivityCategory ActivityUnBanned = null;

        int maxValue = ListOfBannableActivities.ActivityCategories.Count;

        // choose unban
        UnBanValue = Random.Range(0, maxValue);
        ActivityUnBanned = ListOfBannableActivities.ActivityCategories[UnBanValue];

        // choose ban1
        BanValue1 = Random.Range(0, maxValue);
        ActivityBanned1 = ListOfBannableActivities.ActivityCategories[BanValue1];

        // choose ban2
        BanValue2 = Random.Range(0, maxValue);
        ActivityBanned2 = ListOfBannableActivities.ActivityCategories[BanValue2];
            
        // if unban == ban, no unban
        if (UnBanValue == BanValue1 || UnBanValue == BanValue2 )
        {
            UnBanValue = -1;
        }

        ban(ActivityBanned1);
        ban(ActivityBanned2);

        if (UnBanValue >= 0)
        {
            unban(ActivityUnBanned);
        }

    }

    // Gets the position of an activity in the Banned activity list
    private int getPos(Activity activity)
    {
        return BannedActivities.IndexOf(activity);
    }

    //unbans activities in the given ActivityCategory
    private void unban(ActivityCategory AC)
    {
        bool changed = false;
        Activity act;
        int index = -1;

        // check each activity if banned
        for (int i = 0; i < AC.Activities.Count; i++)
        {
            // check if activity banned
            act = AC.Activities[i];
            if (BannedActivities.Contains(act))
            {
                index = getPos(act);
                changed = true;
                BannedActivities.RemoveAt(index);
                BanLevels.RemoveAt(index);
                Debug.Log(act + "is no longer banned");
            }
        }
        if (changed)
        {
            Debug.Log(AC.UnBanText + " " + AC.GroupName + " is no longer banned.");
        }
        else
        {
            Debug.Log("The 'Boss' has itchy fingers.");
        }
    }

    //bans activities in the given ActivityCategory
    private void ban(ActivityCategory AC)
    {
        bool changed = false;
        Activity act;
        int index = -1;

        // check each activity if unbanned
        for (int i = 0; i < AC.Activities.Count; i++)
        {
            // check if activity unbanned
            act = AC.Activities[i];
            if (BannedActivities.Contains(act))
            {
                index = getPos(act);
                BanLevels[index] = BanLevels[index] + 1;
                Debug.Log(act + " is now banned at level " + BanLevels[index]);
            }
            else
            {
                changed = true;
                BannedActivities.Add(act);
                BanLevels.Add(1);
                Debug.Log(act + " is now banned at level 1");
            }
        }
        if (changed) // new addition to list
        {
            Debug.Log(AC.BanText + " " + AC.GroupName + " is now banned.");
        }
        else // already on list
        {
            Debug.Log(AC.MoreBanText + " " + AC.GroupName + " is now more strongly banned.");
        }
    }

}
