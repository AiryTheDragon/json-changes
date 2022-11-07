using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarnAchievementLockedIn : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            AchievementItem achItem = AchievementList.GetItem(Achievement.LockedIn);
            if (!achItem.isDone)
            {
                AchievementList.MakeAchievement(achItem, player.achievementList);
            }
        }
    }
}
