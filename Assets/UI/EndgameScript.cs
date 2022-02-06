using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndgameScript : MonoBehaviour
{
    public TextMeshProUGUI LetterText;

    public GameObject NextPanel;

    // Start is called before the first frame update
    void Start()
    {
        if(Player.Score <= 0)
        {
            LetterText.text = TextFailure();
        }
        else if (Player.Score <= 3)
        {
            LetterText.text = TextAlmostBad();
        }
        else if (Player.Score <= 6)
        {
            LetterText.text = TextBad();
        }
        else if (Player.Score <= 9)
        {
            LetterText.text = TextAlmostGood();
        }
        else if (Player.Score > 9)
        {
            LetterText.text = TextGood();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextPanelClick()
    {
        NextPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
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
}
