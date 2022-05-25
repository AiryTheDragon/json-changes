using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementList : MonoBehaviour
{
    public List<AchievementItem> achievementList = new List<AchievementItem>();

    public void Start()
    {
        createAchievementList();
    }

    public void createAchievementList()
    {
        AchievementItem Item = AchievementItem.CreateInstance<AchievementItem>();
        // Mike and Daniel achievement
        Item.AchievementName = "Win over the creators";
        Item.AchievementDescription = "Achieve full support of Mike and Daniel.";
        Item.isDone = false;

        achievementList.Add(Item);
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
        // do something!
    }
}
