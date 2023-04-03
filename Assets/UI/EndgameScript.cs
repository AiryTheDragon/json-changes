using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Assets.Scenes.ActualGame;

public class EndgameScript : MonoBehaviour
{
    public TextMeshProUGUI LetterText;

    public GameObject NextPanel;
  
    public GameObject WhiskAwayImage;
    public GameObject IsolationImage;
    public GameObject ExecutedImage;
    public GameObject BatteredButFreeImage;
    public GameObject VictoriousImage;
    public GameObject SilverTongueImage;

    public CreditsScroll Credits;
    public EndGameMusicController endGameMusicController;

    private int MaxScore = 78;


    // Start is called before the first frame update
    void Start()
    {
        // create list if it doesn't exist, mostly for testing purposes on credits scene
        AchievementList achList = new();
        if (!AchievementList.Initialized)
        {
            achList.Start();
        }

        if (Player.Score <= (int)(.1 * MaxScore))
        {
            AchievementItem achItem = AchievementList.GetItem(Achievement.WhiskedAway);
            if (!achItem.isDone)
            {
                achList.TryGetAchievement(Achievement.WhiskedAway);
            }
            LetterText.text = TextFailure(achItem);
            endGameMusicController.Play(EndGameMusicListEnum.WhiskedAway);
            ClearPanels();
            WhiskAwayImage.SetActive(true);            
        }
        else if (Player.Score <= (int)(.25 * MaxScore))
        {
            AchievementItem achItem = AchievementList.GetItem(Achievement.Isolation);
            if (!achItem.isDone)
            {
                achList.TryGetAchievement(Achievement.Isolation);
            }
            LetterText.text = TextAlmostBad(achItem);
            endGameMusicController.Play(EndGameMusicListEnum.Isolation); 
            ClearPanels();
            IsolationImage.SetActive(true);
        }
        else if (Player.Score <= (int)(.4 * MaxScore))
        {
            AchievementItem achItem = AchievementList.GetItem(Achievement.Executed);
            if (!achItem.isDone)
            {
                achList.TryGetAchievement(Achievement.Executed);
            }
            endGameMusicController.Play(EndGameMusicListEnum.Executed); 
            LetterText.text = TextBad(achItem);
            ClearPanels();
            ExecutedImage.SetActive(true);
        }
        else if (Player.Score <= (int)(.6 * MaxScore))
        {
            AchievementItem achItem = AchievementList.GetItem(Achievement.BatteredButFree);
            if (!achItem.isDone)
            {
                achList.TryGetAchievement(Achievement.BatteredButFree);
            }
            endGameMusicController.Play(EndGameMusicListEnum.BatteredButFree); 
            LetterText.text = TextAlmostGood(achItem);
            ClearPanels();
            BatteredButFreeImage.SetActive(true);
        }
        else if (Player.Score < MaxScore)
        {
            AchievementItem achItem = AchievementList.GetItem(Achievement.Victorious);
            if (!achItem.isDone)
            {
                achList.TryGetAchievement(Achievement.Victorious);
            }
            endGameMusicController.Play(EndGameMusicListEnum.Victorious); 
            LetterText.text = TextGood(achItem);
            ClearPanels();
            VictoriousImage.SetActive(true);
        }
        else
        {
            AchievementItem achItem = AchievementList.GetItem(Achievement.SilverTongue);
            if (!achItem.isDone)
            {
                achList.TryGetAchievement(Achievement.SilverTongue);
            }
            endGameMusicController.Play(EndGameMusicListEnum.SilverTongue); 
            LetterText.text = TextAwesome(achItem);
            ClearPanels();
            SilverTongueImage.SetActive(true);          
        }
    }

    
    public void NextPanelClick()
    {
        NextPanel.SetActive(true);
        gameObject.SetActive(false);
        Credits.isScrolling = true;
        endGameMusicController.Play(EndGameMusicListEnum.Dreams);

    }

    public void ClearPanels()
    {
    WhiskAwayImage.SetActive(false);
    IsolationImage.SetActive(false);
    ExecutedImage.SetActive(false);
    BatteredButFreeImage.SetActive(false);
    VictoriousImage.SetActive(false);
    SilverTongueImage.SetActive(false);
}

    public void ExitGame()
    {
        SceneManager.LoadScene("StartScene");
    }

    public string TextFailure(AchievementItem item)
    {
        return $"Dear {Player.Name},\n\nAfter much debate in our offices, we have decided the best course of action to take with one so " +
            "foolish as you is to quietly whisk you away.\n\nI hope you enjoy the rest of your life in prison.\n\nSuffer for the Greater Good,\n" +
            "Your Local Area Manager." +
            $"\n\nAchievement:  {item.AchievementName}";
    }

    public string TextAlmostBad(AchievementItem item)
    {
        return $"Dear {Player.Name},\n\nHow you convinced those poor fools to follow you I will always wonder." +
            "\n\nThis will be the last communication you hear from us. As a dangerous entity, you have been assigned strict isolation." +
            "\n\nEnjoy your Private Cell,\nYour Local Area Manager." +
            $"\n\nAchievement:  {item.AchievementName}";
    }

    public string TextBad(AchievementItem item)
    {
        return $"Dear {Player.Name},\n\nWith such a bloody revolt as offered by yourself, we have no choice but to execute you for your crimes.\n\n" +
            "A date for your life's end will be set in the future.\n\n" +
            "Pray during your Last Moments,\nYour Local Area Manager." +
            $"\n\nAchievement:  {item.AchievementName}";
    }

    public string TextAlmostGood(AchievementItem item)
    {
        return $"Dear {Player.Name},\nWe thank you for inciting revolution and leading us to take back our lives and humanity.\n\n" +
            "We ask that you continue to help in every way you can to care for the wounded, sick, and dying. It is unfortunate the results were so bloody.\n\n" +
            "Thank you,\nA New Community." +
            $"\n\nAchievement:  {item.AchievementName}";
    }

    public string TextGood(AchievementItem item)
    {
        return $"Dear PlayerName,\nThank you for leading us to a bright new future! With your marvelous plotting, we were able to peacefully " +
                "transition by putting the few set in their old ways to rest.\n\nWe hope your skill and ideas will continue to be a beacon of hope." +
                "\n\nThank You,\nA New Community." +
                $"\n\nAchievement:  {item.AchievementName}";
    }

    public string TextAwesome(AchievementItem item)
    {
        return $"Dear PlayerName,\nWe are in awe of your skill and wisdom at how you managed to gain the confidence and support of the " + 
                "entire community! Thank you for leading us to a bright new future!" +
                "\n\nWe have confidence in your skill and know your ideas will be a lasting beacon of hope to all of us." +
                "\nThank You,\nA New Community." +
                $"\n\nAchievement:  {item.AchievementName}";
    }




}
