using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;

public class SettingsPanelBehavior : MonoBehaviour
{ 
    public GameObject NextPanel;
    public AudioSource audioSource;

    public GameObject Background;
    public GameObject ReturnButton;
    public GameObject SaveButton;

    public GameObject ResolutionDropDown;
    public GameObject SpeechBubbleSizeDropDown;
    

    public void Start()
    {
        var settings = new GeneralSettings();
        settings.LoadSettings();

        /*
        switch(GeneralSettings.Settings.ScreenSize)
        {
            case Resolutions.R1920X1080:
                Background.GetComponent<Transform>().localScale = new Vector3(1, 1.1f, 1);

                ReturnButton.GetComponent<Transform>().localPosition = new Vector3(0, -438, 0);
                ReturnButton.GetComponent<RectTransform>().sizeDelta = new Vector2(235, 128);
                ReturnButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 67;
                break;
            case Resolutions.R1280X1024:
                Background.GetComponent<Transform>().localScale = new Vector3(1, 1.1f, 1);

                ReturnButton.GetComponent<Transform>().localPosition = new Vector3(0, -438, 0);
                ReturnButton.GetComponent<RectTransform>().sizeDelta = new Vector2(235, 128);
                ReturnButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 67;
                break;
            case Resolutions.R3840X2160:
                Background.GetComponent<Transform>().localScale = new Vector3(2, 2.2f, 1);

                ReturnButton.GetComponent<Transform>().localPosition = new Vector3(0, -918, 0);
                ReturnButton.GetComponent<RectTransform>().sizeDelta = new Vector2(470, 256);
                ReturnButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 134;
                break;
        }
        */
        
    }

    public void OnEnable()
    {
        /*var saveButton = uiDoc.rootVisualElement.Q<UnityEngine.UIElements.Button>("SaveButton");
        saveButton.RegisterCallback<ClickEvent>(SaveButtonClick);
        saveButton.SetEnabled(true);

        var cancelButton = uiDoc.rootVisualElement.Q<UnityEngine.UIElements.Button>("CancelButton");
        cancelButton.RegisterCallback<ClickEvent>(CancelButtonClick);
        cancelButton.SetEnabled(true);*/
    }

    public void ReturnButtonClick()
    {
        audioSource.Play();
        gameObject.SetActive(false);
        NextPanel.SetActive(true);
    }

    public void SaveButtonClick()
    {
        var resDropDown = ResolutionDropDown.GetComponent<TMP_Dropdown>();
        Enum.TryParse(resDropDown.options[resDropDown.value].text, out Resolutions newResolution);

        var speechsizeDropDown = SpeechBubbleSizeDropDown.GetComponent<TMP_Dropdown>();
        string newSpeechBubbleSize = speechsizeDropDown.options[speechsizeDropDown.value].text;

        var settings = new GeneralSettings();
        settings.LoadSettings();
        GeneralSettings.Settings.SpeechBubbleSize = newSpeechBubbleSize;
        GeneralSettings.Settings.ScreenSize = newResolution;
        settings.SaveSettings();

        
    }

}