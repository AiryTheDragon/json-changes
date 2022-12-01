using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotePanelBehavior : MonoBehaviour
{ 
    public GameObject NextPanel;
    public AudioSource audioSource;

    public GameObject Background;
    public GameObject Paper;
    public GameObject Text;
    public GameObject NextButton;

    public void Start()
    {
        var settings = new GeneralSettings();
        settings.LoadSettings();

        switch(GeneralSettings.Settings.ScreenSize)
        {
            case Resolutions.R1920X1080:
                Background.GetComponent<Transform>().localScale = new Vector3(1, 1.1f, 1);
                Text.GetComponent<Transform>().localPosition = new Vector3(0.8032f, 58.0265f, 0);
                Text.GetComponent<RectTransform>().sizeDelta = new Vector2(558.29f, 766.5801f);
                Text.GetComponent<TextMeshProUGUI>().fontSize = 36;

                Paper.GetComponent<Transform>().localPosition = new Vector3(0, 58, 0);
                Paper.GetComponent<Transform>().localScale = new Vector3(0.8345f, 0.727f, 1);

                NextButton.GetComponent<Transform>().localPosition = new Vector3(0, -438, 0);
                NextButton.GetComponent<RectTransform>().sizeDelta = new Vector2(235, 128);
                NextButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 67;
                break;
            case Resolutions.R1280X1024:
                Background.GetComponent<Transform>().localScale = new Vector3(1, 1.1f, 1);
                Text.GetComponent<Transform>().localPosition = new Vector3(0.8032f, 58.0265f, 0);
                Text.GetComponent<RectTransform>().sizeDelta = new Vector2(558.29f, 766.5801f);
                Text.GetComponent<TextMeshProUGUI>().fontSize = 36;

                Paper.GetComponent<Transform>().localPosition = new Vector3(0, 58, 0);
                Paper.GetComponent<Transform>().localScale = new Vector3(0.8345f, 0.727f, 1);

                NextButton.GetComponent<Transform>().localPosition = new Vector3(0, -438, 0);
                NextButton.GetComponent<RectTransform>().sizeDelta = new Vector2(235, 128);
                NextButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 67;
                break;
            case Resolutions.R3840X2160:
                Background.GetComponent<Transform>().localScale = new Vector3(2, 2.2f, 1);
                Text.GetComponent<Transform>().localPosition = new Vector3(0.8032f, 58.0265f, 0);
                Text.GetComponent<RectTransform>().sizeDelta = new Vector2(1116.58f, 1533.1602f);
                Text.GetComponent<TextMeshProUGUI>().fontSize = 72;

                Paper.GetComponent<Transform>().localPosition = new Vector3(0, 58, 0);
                Paper.GetComponent<Transform>().localScale = new Vector3(1.669f, 1.454f, 1);

                NextButton.GetComponent<Transform>().localPosition = new Vector3(0, -918, 0);
                NextButton.GetComponent<RectTransform>().sizeDelta = new Vector2(470, 256);
                NextButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 134;
                break;
        }
    }

    public void Submit()
    {
        audioSource.Play();
        gameObject.SetActive(false);
        NextPanel.SetActive(true);
    }

}