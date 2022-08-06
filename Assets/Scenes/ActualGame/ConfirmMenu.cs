using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmMenu : MonoBehaviour
{

    public IConfirmScript script;
    public TextMeshProUGUI textObject;


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


    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }
}
