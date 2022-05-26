using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementList : MonoBehaviour
{
    public List<AchievementItem> achievementList = new List<AchievementItem>();
    public Player player;

    public void Start()
    {
        createAchievementList();
    }

    public void createAchievementList()
    {
        AchievementItem Item1 = AchievementItem.CreateInstance<AchievementItem>();
        // Mike and Daniel support achievement
        Item1.AchievementName = "Win over the creators";
        Item1.AchievementDescription = "Achieve full support of Mike and Daniel.";
        Item1.isDone = false;
        achievementList.Add(Item1);

        AchievementItem Item2 = AchievementItem.CreateInstance<AchievementItem>();
        // Airy support achievement
        Item2.AchievementName = "Tame the dragon";
        Item2.AchievementDescription = "Achieve full support of Airy.";
        Item2.isDone = false;
        achievementList.Add(Item2);

        AchievementItem Item3 = AchievementItem.CreateInstance<AchievementItem>();
        // Don and Onna support achievement
        Item3.AchievementName = "Parential approval";
        Item3.AchievementDescription = "Achieve full support of Don and Onna.";
        Item3.isDone = false;
        achievementList.Add(Item3);

        AchievementItem Item4 = AchievementItem.CreateInstance<AchievementItem>();
        // Guard caught achievement
        Item4.AchievementName = "Catch the guards";
        Item4.AchievementDescription = "Catch a guard in an illegal act.";
        Item4.isDone = false;
        achievementList.Add(Item4);

        AchievementItem Item5 = AchievementItem.CreateInstance<AchievementItem>();
        // George caught achievement
        Item5.AchievementName = "Hypocrisy";
        Item5.AchievementDescription = "Catch George in an illegal act.";
        Item5.isDone = false;
        achievementList.Add(Item5);

        AchievementItem Item6 = AchievementItem.CreateInstance<AchievementItem>();
        // Manager George support achievement
        Item6.AchievementName = "Manage the manager";
        Item6.AchievementDescription = "Achieve full support of George.";
        Item6.isDone = false;
        achievementList.Add(Item6);
    }

    // Searches for an achievement by its position in the list.
    public AchievementItem getItem(int number)
    {
        AchievementItem Item = null;
        if (number < achievementList.Count && number>=0)
        {
            Item = achievementList[number];
        }
        return Item;
    }

    // Searches for an achievement by string name
    // if found, it returns the achievement, if not, it returns null.
    public AchievementItem getItem(string name)
    {
        AchievementItem Item = null;
        bool found = false;
        for (int i=0; i<achievementList.Count && !found; i++)
        {
            if (achievementList[i].AchievementName.Equals(name))
            {
                Item = achievementList[i];
                found = true;
            }
        }
        return Item;
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
        player.NPCInfoUI.AchievementInfo(item);
        // do something!
    }
}
