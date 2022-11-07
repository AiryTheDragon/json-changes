using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarnAchievementPress : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {Player player = collision.GetComponent<Player>();
            AchievementItem achItem = AchievementList.GetItem(Achievement.MeetThePress);
            if (!achItem.isDone)
            {
                AchievementList.MakeAchievement(achItem, player.achievementList);
            }
        }
    }  
}
