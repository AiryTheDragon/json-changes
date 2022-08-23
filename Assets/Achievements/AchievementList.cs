using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AchievementList : MonoBehaviour
{
    public static List<AchievementItem> AllAchievements { get; set; }= new();
    public Player player;

    public void Start()
    {
        createAchievementList();
    }

    public void createAchievementList()
    {
        AchievementItem Item1 = AchievementItem.CreateInstance<AchievementItem>();
        // Mike and Daniel support achievement
        Item1.AchievementType = Achievement.WinOverTheCreators;
        Item1.AchievementName = "Win over the creators";
        Item1.AchievementDescription = "Achieve full support of Mike and Daniel.";
        Item1.isDone = false;
        AllAchievements.Add(Item1);

        AchievementItem Item2 = AchievementItem.CreateInstance<AchievementItem>();
        // Airy support achievement
        Item2.AchievementType = Achievement.TameTheDragon;
        Item2.AchievementName = "Tame the dragon";
        Item2.AchievementDescription = "Achieve full support of Airy.";
        Item2.isDone = false;
        AllAchievements.Add(Item2);

        AchievementItem Item3 = AchievementItem.CreateInstance<AchievementItem>();
        // Don and Onna support achievement
        Item3.AchievementType = Achievement.ParentialApproval;
        Item3.AchievementName = "Parential approval";
        Item3.AchievementDescription = "Achieve full support of Don and Onna.";
        Item3.isDone = false;
        AllAchievements.Add(Item3);

        AchievementItem Item4 = AchievementItem.CreateInstance<AchievementItem>();
        // Guard caught achievement
        Item4.AchievementType = Achievement.CatchTheGuards;
        Item4.AchievementName = "Catch the guards";
        Item4.AchievementDescription = "Catch a guard in an illegal act.";
        Item4.isDone = false;
        AllAchievements.Add(Item4);

        AchievementItem Item5 = AchievementItem.CreateInstance<AchievementItem>();
        // George caught achievement
        Item5.AchievementType = Achievement.Hypocrisy;
        Item5.AchievementName = "Hypocrisy";
        Item5.AchievementDescription = "Catch George in an illegal act.";
        Item5.isDone = false;
        AllAchievements.Add(Item5);

        AchievementItem Item6 = AchievementItem.CreateInstance<AchievementItem>();
        // Manager George support achievement
        Item6.AchievementType = Achievement.ManageTheManager;
        Item6.AchievementName = "Manage the manager";
        Item6.AchievementDescription = "Achieve full support of George.";
        Item6.isDone = false;
        AllAchievements.Add(Item6);

        AchievementItem Item7 = AchievementItem.CreateInstance<AchievementItem>();
        // Writing letter achievement
        Item7.AchievementType = Achievement.MightierThanTheSword;
        Item7.AchievementName = "Mightier than the sword";
        Item7.AchievementDescription = "Write a letter.";
        Item7.isDone = false;
        AllAchievements.Add(Item7);

        AchievementItem Item8 = AchievementItem.CreateInstance<AchievementItem>();
        // Touching lava achievement
        Item8.AchievementType = Achievement.Burned;
        Item8.AchievementName = "Burned";
        Item8.AchievementDescription = "Touch the lava.";
        Item8.isDone = false;
        AllAchievements.Add(Item8);

        AchievementItem Item9 = AchievementItem.CreateInstance<AchievementItem>();
        // Enter Jail achievement
        Item9.AchievementType = Achievement.SightseeThePrison;
        Item9.AchievementName = "Sightsee the prison";
        Item9.AchievementDescription = "Enter the jail.";
        Item9.isDone = false;
        AllAchievements.Add(Item9);

        AchievementItem Item10 = AchievementItem.CreateInstance<AchievementItem>();
        // Enter Jail cell achievement
        Item10.AchievementType = Achievement.LockedIn;
        Item10.AchievementName = "Locked in";
        Item10.AchievementDescription = "Enter the jail cell.";
        Item10.isDone = false;
        AllAchievements.Add(Item10);

        AchievementItem Item11 = AchievementItem.CreateInstance<AchievementItem>();
        // Enter the manager House
        Item11.AchievementType = Achievement.AreYouAllowedInHere;
        Item11.AchievementName = "Are you allowed in here?";
        Item11.AchievementDescription = "Enter the manager's house.";
        Item11.isDone = false;
        AllAchievements.Add(Item11);

        AchievementItem Item12 = AchievementItem.CreateInstance<AchievementItem>();
        // Enter the printing press room
        Item12.AchievementType = Achievement.MeetThePress;
        Item12.AchievementName = "Meet the press";
        Item12.AchievementDescription = "Enter the printing press room.";
        Item12.isDone = false;
        AllAchievements.Add(Item12);

        AchievementItem Item13 = AchievementItem.CreateInstance<AchievementItem>();
        // Less than 10% support
        Item13.AchievementType = Achievement.WhiskedAway;
        Item13.AchievementName = "Whisked away";
        Item13.AchievementDescription = "Attempt a rebellion with marginal support.";
        Item13.isDone = false;
        AllAchievements.Add(Item13);

        AchievementItem Item14 = AchievementItem.CreateInstance<AchievementItem>();
        // 10 - 25% support
        Item14.AchievementType = Achievement.Isolation;
        Item14.AchievementName = "Isolation";
        Item14.AchievementDescription = "Attempt a rebellions with a small amount of support.";
        Item14.isDone = false;
        AllAchievements.Add(Item14);

        AchievementItem Item15 = AchievementItem.CreateInstance<AchievementItem>();
        // 25% - 40% support
        Item15.AchievementType = Achievement.Executed;
        Item15.AchievementName = "Executed";
        Item15.AchievementDescription = "Attempt a rebellion with moderate support.";
        Item15.isDone = false;
        AllAchievements.Add(Item15);

        AchievementItem Item16 = AchievementItem.CreateInstance<AchievementItem>();
        // 40 - 60% support
        Item16.AchievementType = Achievement.BatteredButFree;
        Item16.AchievementName = "Battered but free";
        Item16.AchievementDescription = "Attempt a rebellion with sufficient support.";
        Item16.isDone = false;
        AllAchievements.Add(Item16);

        AchievementItem Item17 = AchievementItem.CreateInstance<AchievementItem>();
        // 60 - 99% support
        Item17.AchievementType = Achievement.Victorious;
        Item17.AchievementName = "Victorious";
        Item17.AchievementDescription = "Attempt a rebellion with major support.";
        Item17.isDone = false;
        AllAchievements.Add(Item17);

        AchievementItem Item18 = AchievementItem.CreateInstance<AchievementItem>();
        // 100% support
        Item18.AchievementType = Achievement.SilverTongue;
        Item18.AchievementName = "Silver tongue";
        Item18.AchievementDescription = "Attempt a rebellion with complete support.";
        Item18.isDone = false;
        AllAchievements.Add(Item18);

        AchievementItem Item19 = AchievementItem.CreateInstance<AchievementItem>();
        // Hoard at least 10 pens and pieces of paper
        Item19.AchievementType = Achievement.Hoarder;
        Item19.AchievementName = "Hoarder";
        Item19.AchievementDescription = "Hold at least 10 pens and pieces of paper.";
        Item19.isDone = false;
        AllAchievements.Add(Item19);

        AchievementItem Item20 = AchievementItem.CreateInstance<AchievementItem>();
        // Pee in the bushes
        Item20.AchievementType = Achievement.Aaaaaaaaaah;
        Item20.AchievementName = "Aaaaaaaaaah";
        Item20.AchievementDescription = "Pee in the bushes.";
        Item20.isDone = false;
        AllAchievements.Add(Item20);

        AchievementItem Item21 = AchievementItem.CreateInstance<AchievementItem>();
        // Pee in the bushes
        Item21.AchievementType = Achievement.NoMoreGames;
        Item21.AchievementName = "No more games!";
        Item21.AchievementDescription = "Deliver a letter warning about programming video games.";
        Item21.isDone = false;
        AllAchievements.Add(Item21);

    }

    // Searches for an achievement by its position in the list.
    public AchievementItem getItem(int number)
    {
        AchievementItem Item = null;
        if (number < AllAchievements.Count && number>=0)
        {
            Item = AllAchievements[number];
        }
        return Item;
    }

    // Searches for an achievement by string name
    // if found, it returns the achievement, if not, it returns null.
    public AchievementItem getItem(Achievement ach)
    {
        AchievementItem Item = null;
        bool found = false;
        for (int i=0; i<AllAchievements.Count && !found; i++)
        {
            if (AllAchievements[i].AchievementType == ach)
            {
                Item = AllAchievements[i];
                found = true;
            }
        }
        return Item;
    }

    public void CheckMoralAchievements(NPCBehavior npc)
    {
        var player = GameObject.FindObjectOfType<Player>();

        // check morale achievements
        if (npc.ManipulationLevel >= 5)
        {
            
            bool maxMike = false;
            bool maxDaniel = false;
            bool maxOnna = false;
            bool maxDonald = false;
            foreach (var person in player.PeopleKnown.Values)
            {
                if (person.Name.Equals("Mike") && person.ManipulationLevel >= 5)
                {
                    maxMike = true;
                    Debug.Log("Mike is maxed");
                }
                if (person.Name.Equals("Daniel") && person.ManipulationLevel >= 5)
                {
                    maxDaniel = true;
                    Debug.Log("Daniel is maxed");
                }
                if (person.Name.Equals("Onna") && person.ManipulationLevel >= 5)
                {
                    maxOnna = true;
                    Debug.Log("Onna is maxed");
                }
                if (person.Name.Equals("Donald") && person.ManipulationLevel >= 5)
                {
                    maxDonald = true;
                    Debug.Log("Donald is maxed");
                }
            }
            if (maxMike && maxDaniel)
            {
                AchievementItem achItem = getItem(Achievement.WinOverTheCreators);
                if (!achItem.isDone)
                {
                    makeAchievement(achItem);
                }
            }
            if (maxOnna && maxDonald)
            {
                AchievementItem achItem = getItem(Achievement.ParentialApproval);
                if (!achItem.isDone)
                {
                    makeAchievement(achItem);
                }
            }
            if (npc.Name.Equals("Airy"))
            {
                AchievementItem achItem = getItem(Achievement.TameTheDragon);
                if (!achItem.isDone)
                {
                    makeAchievement(achItem);
                }
            }
            if (npc.Name.Equals("Manager George"))
            {
                AchievementItem achItem = getItem(Achievement.ManageTheManager);
                if (!achItem.isDone)
                {
                    makeAchievement(achItem);
                }
            }
        }            
        if (npc.Name.Equals("Manager George"))
        {
            AchievementItem achItem = getItem(Achievement.Hypocrisy);
            if (!achItem.isDone)
            {
                makeAchievement(achItem);
            }
        }
        if (npc.tag.Equals("GuardNPC"))
        {
            AchievementItem achItem = getItem(Achievement.CatchTheGuards);
            if (!achItem.isDone)
            {
                makeAchievement(achItem);
            }
        }
    }

    /// <summary>
    /// The method should check the type of ban for the letter and see if an achievement should be awarded
    /// </summary>
    /// <param name="letter"></param>
    public void CheckLetterTypeAchievements(Letter letter)
    {
        /*TODO
        if (letter.banType=="Working on game");
        */
    }


    public void TryGetAchievement(Achievement ach)
    {
        var achievement = AllAchievements.First(x => x.AchievementType == ach);
        if(!achievement.isDone)
        {
            achievement.isDone = true;
            awardAchievement(achievement);
        }
    }

    public void makeAchievement(AchievementItem item)
    {
        if (!item.isDone)
        {
            item.isDone = true;
            awardAchievement(item);
        }
    }

    private void awardAchievement(AchievementItem item)
    {
        if (player != null)
        {
            player.NPCInfoUI.AchievementInfo(item);
            player.log.AddItem("Achievement", $"New Achievement:  {item.AchievementName}\nDescription:   {item.AchievementDescription}");
        }
        else
        {
            Debug.Log("Made achievement " + item.AchievementName);
        }
        // do something!
    }
}
