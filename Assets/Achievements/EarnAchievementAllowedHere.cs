using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarnAchievementAllowedHere : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            AchievementItem achItem = player.achievementList.getItem(Achievement.AreYouAllowedInHere);
            if (!achItem.isDone)
            {
                player.achievementList.makeAchievement(achItem);
            }
        }
    }  
}
