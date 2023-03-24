using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Steamworks;
using Assets.Scenes.ActualGame;

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
        public GameObject CreditsButton;
        public GameObject Text;
        public GameObject Background;

        public CreditsScroll Credits;
        // BtnSelectGame variables.
        public GameObject SelectGamePanel;

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
                    PlayButton.GetComponent<Transform>().localPosition = new Vector3(-500, 100, 0);
                    PlayButton.GetComponent<RectTransform>().sizeDelta = new Vector2(710, 170);
                    PlayButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 140;
                    OptionsButton.GetComponent<Transform>().localPosition = new Vector3(-500, -150, 0);
                    OptionsButton.GetComponent<RectTransform>().sizeDelta = new Vector2(710, 170);
                    OptionsButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 140;
                    HelpButton.GetComponent<Transform>().localPosition = new Vector3(500, 100, 0);
                    HelpButton.GetComponent<RectTransform>().sizeDelta = new Vector2(710, 170);
                    HelpButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 140;
                    ExitButton.GetComponent<Transform>().localPosition = new Vector3(500, -150, 0);
                    ExitButton.GetComponent<RectTransform>().sizeDelta = new Vector2(710, 170);
                    ExitButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 140;
                    CreditsButton.GetComponent<Transform>().localPosition = new Vector3(0, -400, 0);
                    CreditsButton.GetComponent<RectTransform>().sizeDelta = new Vector2(710, 170);
                    CreditsButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 140;

                    Background.GetComponent<Transform>().localPosition = new Vector3(0, 0, 0);
                    Background.GetComponent<Transform>().localScale = new Vector3(1, 1.1f, 1);
                    Text.GetComponent<Transform>().localPosition = new Vector3(0, 220, 0);
                    Text.GetComponent<TextMeshProUGUI>().fontSize = 189;
                    break;
                case Resolutions.R1280X1024:
                    PlayButton.GetComponent<Transform>().localPosition = new Vector3(-300, 100, 0);
                    PlayButton.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 170);
                    PlayButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 140;
                    OptionsButton.GetComponent<Transform>().localPosition = new Vector3(-300, -150, 0);
                    OptionsButton.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 170);
                    OptionsButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 140;
                    HelpButton.GetComponent<Transform>().localPosition = new Vector3(300, 1000, 0);
                    HelpButton.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 170);
                    HelpButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 140;
                    ExitButton.GetComponent<Transform>().localPosition = new Vector3(300, -150, 0);
                    ExitButton.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 170);
                    ExitButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 140;
                    CreditsButton.GetComponent<Transform>().localPosition = new Vector3(0, -400, 0);
                    CreditsButton.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 170);
                    CreditsButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 140;

                    Background.GetComponent<Transform>().localPosition = new Vector3(0, 0, 0);
                    Background.GetComponent<Transform>().localScale = new Vector3(1, 1.1f, 1);
                    Text.GetComponent<Transform>().localPosition = new Vector3(0, 220, 0);
                    Text.GetComponent<TextMeshProUGUI>().fontSize = 189;
                    break;
                case Resolutions.R3840X2160:
                    PlayButton.GetComponent<Transform>().localPosition = new Vector3(-600, 0, 0);
                    PlayButton.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 340);
                    PlayButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 280;
                    OptionsButton.GetComponent<Transform>().localPosition = new Vector3(-600, -500, 0);
                    OptionsButton.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 340);
                    OptionsButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 280;
                    HelpButton.GetComponent<Transform>().localPosition = new Vector3(600, 0, 0);
                    HelpButton.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 340);
                    HelpButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 280;
                    ExitButton.GetComponent<Transform>().localPosition = new Vector3(600, -500, 0);
                    ExitButton.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 340);
                    ExitButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 280;
                    CreditsButton.GetComponent<Transform>().localPosition = new Vector3(0, -1000, 0);
                    CreditsButton.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 340);
                    CreditsButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 280;

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

        public void BtnCredits()
        {
            if (!Credits.isScrolling)
            {
                Credits.isScrolling = true;
            }
            else
            {
                Credits.isScrolling = false;
                Credits.transform.localPosition = new Vector3(0, -700, 0);
            }

        }

        public void BtnQuit()
        {
            Application.Quit();
        }
    }
}