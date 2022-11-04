using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndgameScript : MonoBehaviour
{
    public TextMeshProUGUI LetterText;

    public GameObject NextPanel;

    private int MaxScore = 78;


    // Start is called before the first frame update
    void Start()
    {
        AchievementList achList = new AchievementList();

        if(Player.Score <= (int)(.1 * MaxScore))
        {
            LetterText.text = TextFailure();
            AchievementItem achItem = achList.getItem(Achievement.WhiskedAway);
            if (!achItem.isDone)
            {
                achList.makeAchievement(achItem);
            }
        }
        else if (Player.Score <= (int)(.25 * MaxScore))
        {
            LetterText.text = TextAlmostBad();
            AchievementItem achItem = achList.getItem(Achievement.Isolation);
            if (!achItem.isDone)
            {
                achList.makeAchievement(achItem);
            }
        }
        else if (Player.Score <= (int)(.4 * MaxScore))
        {
            LetterText.text = TextBad();
            AchievementItem achItem = achList.getItem(Achievement.Executed);
            if (!achItem.isDone)
            {
                achList.makeAchievement(achItem);
            }
        }
        else if (Player.Score <= (int)(.6 * MaxScore))
        {
            LetterText.text = TextAlmostGood();
            AchievementItem achItem = achList.getItem(Achievement.BatteredButFree);
            if (!achItem.isDone)
            {
                achList.makeAchievement(achItem);
            }
        }
        else if (Player.Score < MaxScore)
        {
            LetterText.text = TextGood();
            AchievementItem achItem = achList.getItem(Achievement.Victorious);
            if (!achItem.isDone)
            {
                achList.makeAchievement(achItem);
            }
        }
        else
        {
            LetterText.text = TextAwesome();
            AchievementItem achItem = achList.getItem(Achievement.SilverTongue);
            if (!achItem.isDone)
            {
                achList.makeAchievement(achItem);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void NextPanelClick()
    {
        NextPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public string TextFailure()
    {
        return $"Dear {Player.Name},\n\nAfter much debate in our offices, we have decided the best course of action to take with one so " +
            "foolish as you is to quietly whisk you away.\n\nI hope you enjoy the rest of your life in prison.\n\nSuffer for the Greater Good,\n" +
            "Your Local Area Manager.";
    }

    public string TextAlmostBad()
    {
        return $"Dear {Player.Name},\n\nHow you convinced those poor fools to follow you I will always wonder." +
            "\n\nThis will be the last communication you hear from us. As a dangerous entity, you have been assigned strict isolation." +
            "\n\nEnjoy your Private Cell,\nYour Local Area Manager.";
    }

    public string TextBad()
    {
        return $"Dear {Player.Name},\n\nWith such a bloody revolt as offered by yourself, we have no choice but to execute you for your crimes.\n\n" +
            "A date for your life's end will be set in the future.\n\n" +
            "Pray during your Last Moments,\nYour Local Area Manager.";
    }

    public string TextAlmostGood()
    {
        return $"Dear {Player.Name},\n\nWe thank you for inciting revolution and leading us to take back our lives and humanity.\n\n" +
            "We ask that you continue to help in every way you can to care for the wounded, sick, and dying. It is unfortunate the results were so bloody.\n\n" +
            "Thank you,\nA New Community.";
    }

    public string TextGood()
    {
        return $"Dear PlayerName,\n\nThank you for leading us to a bright new future! With your marvelous plotting, we were able to peacefully " +
                "transition by putting the few set in their old ways to rest.\n\nWe hope your skill and ideas will continue to be a beacon of hope." +   
                "Thank You,\nA New Community.";
    }

    public string TextAwesome()
    {
        return $"Dear PlayerName,\n\nWe are is awe of your skill and wisdom at how you managed to gain the confidence and support of the " + 
                "entire community! Thank you for leading us to a bright new future!" +
                "\n\nWe have confidence in your skill and know your ideas will be a lasting beacon of hope to all of us." +
                "Thank You,\nA New Community.";
    }




}
