using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HelpPanelBehavior : MonoBehaviour
{
    public GameObject ReturnPanel;
    public AudioSource audioSource;

    public GameObject Background;
    public GameObject HelpMenu;
    public GameObject ReturnButton;

    public void Start()
    {
        var settings = new GeneralSettings();
        settings.LoadSettings();

        switch(GeneralSettings.Settings.ScreenSize)
        {
            case Resolutions.R1920X1080:
            case Resolutions.R1280X1024:
                Background.GetComponent<Transform>().localScale = new Vector3(1, 1.1f, 1);
                HelpMenu.GetComponent<Transform>().localPosition = new Vector3(-978.25f, -474.75f, 0);
                HelpMenu.GetComponent<Transform>().localScale = new Vector2(1, 1);

                ReturnButton.GetComponent<Transform>().localPosition = new Vector3(0, -438, 0);
                ReturnButton.GetComponent<RectTransform>().sizeDelta = new Vector2(235, 128);
                ReturnButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 67;
                break;
            case Resolutions.R3840X2160:
                Background.GetComponent<Transform>().localScale = new Vector3(2, 2.2f, 1);
                HelpMenu.GetComponent<Transform>().localPosition = new Vector3(-1917, -903, 0);
                HelpMenu.GetComponent<Transform>().localScale = new Vector2(2, 2);

                ReturnButton.GetComponent<Transform>().localPosition = new Vector3(0, -918, 0);
                ReturnButton.GetComponent<RectTransform>().sizeDelta = new Vector2(470, 256);
                ReturnButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 134;
                break;
        }
    }

    public void Submit()
    {
        audioSource.Play();
        gameObject.SetActive(false);
        ReturnPanel.SetActive(true);
    }
}
