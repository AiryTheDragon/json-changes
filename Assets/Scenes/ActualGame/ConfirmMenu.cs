using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmMenu : MonoBehaviour
{

    public IConfirmScript script;
    public TextMeshProUGUI textObject;
    public TextMeshProUGUI yesTextObject;
    public TextMeshProUGUI noTextObject;


    public void Confirm()
    {
        //Do something
        gameObject.SetActive(false);
        script.ConfirmAction();
    }

    public void UpdateText(string text)
    {
        textObject.text = text;
    }

    // Updates the text button to confirm an action
    public void UpdateYesText(string text)
    {
        yesTextObject.text = text;
    }
    // Updates the text button that closes the confirmation box
    public void UpdateNoText(string text)
    {
        noTextObject.text = text;
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }
}
