using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannedActivitiesBehavior : MonoBehaviour, INeedsClockUpdate
{
    public List<Activity> BannedActivities;

    public List<int> BanLevels;

    public BannableActivityList ListOfBannableActivities;

    public Log log;

    private ClockBehavior Clock;

    public GameObject confirmObject;
    public GameObject yesButton;
    public AudioClip _lawNotice;
    private AudioSource _source = null;


    // Start is called before the first frame update
    void Start()
    {
        Clock = GameObject.Find("Clock").GetComponent<ClockBehavior>();
        Clock.NeedsClockUpdate.Add(this);
        _source = GetComponent<AudioSource>();
        if (_source == null)
        {
            Debug.Log("Audio Source is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

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


    public void UpdateClock(ClockTime time)
    {
        // time to ban and unban activities (at 7:00 every day)
        if (time.Minute==0 && time.Hour==7)
        {
            UpdateBans();

        }
    }

    // This method updates the banned activites in the game.
    private void UpdateBans()
    {
        // algorithm to ban and unban activities.  Choose one sleep to ban and one to unban
        int BanValue1 = -1;
        int BanValue2 = -1;

        int UnBanValue = -1;
        ActivityCategory ActivityBanned1 = null;
        ActivityCategory ActivityBanned2 = null;
        ActivityCategory ActivityUnBanned = null;

        log.AddItem("Notice", "Law Updates for Day " + Clock.Time.Day + ".");

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

        NotifyPlayerOfUpdate();

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

        // used to add notification only once in notices
        List<string> unbanText = new List<string>();
        string unbanString = "";

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

                RemoveActivitiesFromSeen(act);
                // add notificiation only once in notices 
                unbanString = act.Name + " is no longer banned.";
                if (!unbanText.Contains(unbanString))
                {
                    unbanText.Add(unbanString);
                    log.AddItem("Unban", unbanString);
                }
            }
        }
        if (changed)
        {
            log.AddItem("The Manager", AC.UnBanText + " " + AC.GroupName + " is no longer banned.");
        }
        else
        {
            log.AddItem("The Manager", "The Manager is happy with the current laws.");
        }
    }

    //bans activities in the given ActivityCategory
    private void ban(ActivityCategory AC)
    {
        bool changed = false;
        Activity act;
        int index = -1;

        // used to add notification only once in notices
        List<string> banText = new List<string>();
        string banString = "";

        // check each activity if unbanned
        for (int i = 0; i < AC.Activities.Count; i++)
        {

            // check if activity unbanned
            act = AC.Activities[i];
            if (BannedActivities.Contains(act))
            {
                index = getPos(act);
                BanLevels[index] = BanLevels[index] + 1;

                // add notificiation only once in notices 
                banString = act.Name + " is now banned at level " + BanLevels[index] + ".";
                if (!banText.Contains(banString))
                {
                    banText.Add(banString);
                    Debug.Log(banString);
                    log.AddItem("Tougher ban", banString);
                }
            }
            else
            {
                changed = true;
                BannedActivities.Add(act);
                BanLevels.Add(1);

                // add notificiation only once in notices 
                banString = act.Name + " is now banned at level 1.";
                if (!banText.Contains(banString))
                {
                    banText.Add(banString);
                    log.AddItem("New ban", banString);
                }
            }
        }
        if (changed) // new addition to list
        {
            log.AddItem("The Manager", AC.BanText + " " + AC.GroupName + " is now banned.");
        }
        else // already on list
        {
            log.AddItem("The Manager", AC.MoreBanText + " " + AC.GroupName + " is now more strongly banned.");
        }
    }

    // This method loads the confirmation notice on the player screen notifying the player that a law update happened
    private void NotifyPlayerOfUpdate()
    {

        confirmObject.GetComponent<ConfirmMenu>().UpdateText("You have received notification from the Manager that new laws were enacted.  You have noted it in your notebook.");
        _source.clip = _lawNotice;
        _source.Play();
        confirmObject.SetActive(true);
        yesButton.SetActive(false); // no additional action can be done
    }

    private void RemoveActivitiesFromSeen(Activity activity)
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
        foreach (Person person in player.PeopleKnown.Values)
        {
            List<SeenActivity> seen = person.SeenActivities;

            for (int i = 0; i < seen.Count; i++)
            {
                if (seen[i].ReadableName.Equals(activity.Name))
                {
                    seen.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
