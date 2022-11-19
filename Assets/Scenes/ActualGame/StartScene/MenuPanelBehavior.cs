using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Steamworks;

namespace StartScene
{
    #if !(UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX || STEAMWORKS_WIN || STEAMWORKS_LIN_OSX)
        #define DISABLESTEAMWORKS
    #endif

    public class MenuPanelBehavior : MonoBehaviour
    {
        public GameObject PlayButton;
        public GameObject OptionsButton;
        public GameObject HelpButton;
        public GameObject ExitButton;
        public GameObject Text;
        public GameObject Background;

        // BtnSelectGame variables.
        public GameObject SelectGamePanel;

        // BtnOptions variables.
        public TextMeshProUGUI OptionsLetterText;

        public GameObject OptionsPanel;

        public AudioSource OptionsAudioSource;

        // BtnHelp variables.
        public GameObject HelpPanel;

        public AudioSource HelpAudioSource;

        // Start is called before the first frame update
        void Start()
        {
            var settings = new GeneralSettings();
            settings.LoadSettings();

            switch(GeneralSettings.Settings.ScreenSize)
            {
                case Resolutions.R1920X1080:
                    PlayButton.GetComponent<Transform>().localPosition = new Vector3(-500, 0, 0);
                    PlayButton.GetComponent<RectTransform>().sizeDelta = new Vector2(710, 170);
                    PlayButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 140;
                    OptionsButton.GetComponent<Transform>().localPosition = new Vector3(-500, -250, 0);
                    OptionsButton.GetComponent<RectTransform>().sizeDelta = new Vector2(710, 170);
                    OptionsButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 140;
                    HelpButton.GetComponent<Transform>().localPosition = new Vector3(500, 0, 0);
                    HelpButton.GetComponent<RectTransform>().sizeDelta = new Vector2(710, 170);
                    HelpButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 140;
                    ExitButton.GetComponent<Transform>().localPosition = new Vector3(500, -250, 0);
                    ExitButton.GetComponent<RectTransform>().sizeDelta = new Vector2(710, 170);
                    ExitButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 140;

                    Background.GetComponent<Transform>().localPosition = new Vector3(0, 0, 0);
                    Background.GetComponent<Transform>().localScale = new Vector3(1, 1.1f, 1);
                    Text.GetComponent<Transform>().localPosition = new Vector3(0, 220, 0);
                    Text.GetComponent<TextMeshProUGUI>().fontSize = 189;
                    break;
                case Resolutions.R1280X1024:
                    PlayButton.GetComponent<Transform>().localPosition = new Vector3(-300, 0, 0);
                    PlayButton.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 170);
                    PlayButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 140;
                    OptionsButton.GetComponent<Transform>().localPosition = new Vector3(-300, -250, 0);
                    OptionsButton.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 170);
                    OptionsButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 140;
                    HelpButton.GetComponent<Transform>().localPosition = new Vector3(300, 0, 0);
                    HelpButton.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 170);
                    HelpButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 140;
                    ExitButton.GetComponent<Transform>().localPosition = new Vector3(300, -250, 0);
                    ExitButton.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 170);
                    ExitButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 140;

                    Background.GetComponent<Transform>().localPosition = new Vector3(0, 0, 0);
                    Background.GetComponent<Transform>().localScale = new Vector3(1, 1.1f, 1);
                    Text.GetComponent<Transform>().localPosition = new Vector3(0, 220, 0);
                    Text.GetComponent<TextMeshProUGUI>().fontSize = 189;
                    break;
                case Resolutions.R3840X2160:
                    PlayButton.GetComponent<Transform>().localPosition = new Vector3(-600, 0, 0);
                    PlayButton.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 540);
                    PlayButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 280;
                    OptionsButton.GetComponent<Transform>().localPosition = new Vector3(-600, -500, 0);
                    OptionsButton.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 540);
                    OptionsButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 280;
                    HelpButton.GetComponent<Transform>().localPosition = new Vector3(600, 0, 0);
                    HelpButton.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 540);
                    HelpButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 280;
                    ExitButton.GetComponent<Transform>().localPosition = new Vector3(600, -500, 0);
                    ExitButton.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 540);
                    ExitButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 280;

                    Background.GetComponent<Transform>().localPosition = new Vector3(0, 0, 0);
                    Background.GetComponent<Transform>().localScale = new Vector3(2, 2.2f, 1);
                    Text.GetComponent<Transform>().localPosition = new Vector3(0, 540, 0);
                    Text.GetComponent<TextMeshProUGUI>().fontSize = 378;
                    break;
            }
        }

        void OnApplicationQuit()
        {
            #if !DISABLESTEAMWORKS
                SteamAPI.Shutdown();
            #endif
        }

        public void BtnSelectGame()
        {
            SelectGamePanel.SetActive(true);
            gameObject.SetActive(false);
        }

        public void BtnOptions()
        {
            OptionsLetterText.SetText($"Dear Player,\n\nThis feature is not implemented yet.  Please choose another option." +
            "\n\nExtra options bring conflict,\nYour Local Area Manager");
            OptionsAudioSource.Play();
            gameObject.SetActive(false);
            OptionsPanel.SetActive(true);
        }

        public void BtnHelp()
        {
            HelpAudioSource.Play();
            gameObject.SetActive(false);
            HelpPanel.SetActive(true);
        }

        public void BtnQuit()
        {
            Application.Quit();
        }
    }
}