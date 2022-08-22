using System.Linq;
using UnityEngine;

public class AchievementSettings
{
    public AchievementSave[] Achievements;

    public void LoadAchievementsFromGame()
    {
        var achievementList = AchievementList.AllAchievements;
        Achievements = new AchievementSave[achievementList.Count];
        for(int i = 0; i < Achievements.Length; i++)
        {
            Achievements[i].Name = achievementList[i].AchievementName;
            Achievements[i].Complete = achievementList[i].isDone;
        }
    }

    public void LoadAchievementFromSave()
    {
        var achievementList = AchievementList.AllAchievements;
        foreach(var achievementSave in Achievements)
        {
            var achievement = achievementList.FirstOrDefault(x => x.AchievementName == achievementSave.Name);
            if(achievement is null || achievement.AchievementName is null)
            {
                continue;
            }
            achievement.isDone = achievementSave.Complete;
        }
    }
}