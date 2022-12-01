using System.ComponentModel;
using System.Net.Mime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class GameSelectionScript : MonoBehaviour
{

    public string SaveLocation;

    public List<GameObject> SlotButtons;

    public GameObject LeftButton;

    public GameObject RightButton;

    private int selectedSlot = 0;

    public GameObject PreviousPanel;

    public GameObject BackButton;

    public GameObject ChooseSaveText;

    public GameObject Background;

    // Start is called before the first frame update
    void Start()
    {
        SaveLocation = System.IO.Path.Combine(Application.persistentDataPath, "EEData/saves");
        LeftButton.SetActive(false);
        RightButton.SetActive(false);

        for(int i = 0; i < 3; i++)
        {
            string saveFilePath = Path.Combine(SaveLocation, $"save{i+1}.json");
            if(!File.Exists(saveFilePath))
            {
                var texts = SlotButtons[i].GetComponentsInChildren<TextMeshProUGUI>();
                texts.First(x => x.name == "NameText").text = "Open";
                texts.First(x => x.name == "DayText").text = "";
            }
            else
            {
                using FileStream file = File.OpenRead(saveFilePath);
                using StreamReader fileReader = new(file);
                string fileText = fileReader.ReadToEnd();
                fileReader.Dispose();
                file.Dispose();
                var save = JsonConvert.DeserializeObject<SaveFile>(fileText);
                if(save is null || save.PlayerName is null)
                {
                    File.Delete(saveFilePath);
                    i--;
                    continue;
                }
                var texts = SlotButtons[i].GetComponentsInChildren<TextMeshProUGUI>();
                texts.First(x => x.name == "NameText").text = save.PlayerName;
                texts.First(x => x.name == "DayText").text = $"Day {save.Time.Day}";
            }
        }

        var settings = new GeneralSettings();
        settings.LoadSettings();

        switch(GeneralSettings.Settings.ScreenSize)
        {
            case Resolutions.R1920X1080:
                {
                    Background.GetComponent<Transform>().localScale = new Vector2(1, 1.1f);

                    BackButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(227, -107, 0);
                    BackButton.GetComponent<RectTransform>().sizeDelta = new Vector2(403, 160);
                    BackButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 140;
                    ChooseSaveText.GetComponent<TextMeshProUGUI>().fontSize = 189;
                    ChooseSaveText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -136, 0);

                    LeftButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(460, 155, 0);
                    LeftButton.GetComponent<RectTransform>().sizeDelta = new Vector2(710, 179);
                    LeftButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 140;

                    RightButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(-460, 155, 0);
                    RightButton.GetComponent<RectTransform>().sizeDelta = new Vector2(710, 179);
                    RightButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 140;

                    int height = -323;
                    foreach(var button in SlotButtons)
                    {
                        button.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, height, 0);
                        button.GetComponent<RectTransform>().sizeDelta = new Vector2(1336.121f, 178.6005f);
                        button.GetComponent<Transform>().localScale = new Vector2(1, 1);
                        height -= 197;
                    }
                    break;
                }
            case Resolutions.R1280X1024:
                {
                    Background.GetComponent<Transform>().localScale = new Vector2(1, 1.1f);

                    BackButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(115, -54, 0);
                    BackButton.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 80);
                    BackButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 67;
                    ChooseSaveText.GetComponent<TextMeshProUGUI>().fontSize = 120;
                    ChooseSaveText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -116, 0);

                    LeftButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(300, 110, 0);
                    LeftButton.GetComponent<RectTransform>().sizeDelta = new Vector2(355, 105);
                    LeftButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 67;

                    RightButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(-300, 110, 0);
                    RightButton.GetComponent<RectTransform>().sizeDelta = new Vector2(355, 105);
                    RightButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 67;

                    int height = -323;
                    foreach(var button in SlotButtons)
                    {
                        button.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, height, 0);
                        button.GetComponent<RectTransform>().sizeDelta = new Vector2(1336.121f, 178.6005f);
                        button.GetComponent<Transform>().localScale = new Vector2(0.8f, 0.8f);
                        height -= 197;
                    }
                    break;
                }
            case Resolutions.R3840X2160:
                {
                    Background.GetComponent<Transform>().localScale = new Vector2(2, 2.2f);
                    //
                    BackButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(340, -180, 0);
                    BackButton.GetComponent<RectTransform>().sizeDelta = new Vector2(550, 260);
                    BackButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 200;
                    ChooseSaveText.GetComponent<TextMeshProUGUI>().fontSize = 320;
                    ChooseSaveText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -200, 0);

                    LeftButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(1100, 275, 0);
                    LeftButton.GetComponent<RectTransform>().sizeDelta = new Vector2(900, 290);
                    LeftButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 200;

                    RightButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(-1100, 275, 0);
                    RightButton.GetComponent<RectTransform>().sizeDelta = new Vector2(900, 290);
                    RightButton.GetComponentInChildren<TextMeshProUGUI>().fontSize = 200;

                    int height = -600;
                    foreach(var button in SlotButtons)
                    {
                        button.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, height, 0);
                        button.GetComponent<RectTransform>().sizeDelta = new Vector2(1336.121f, 178.6005f);
                        button.GetComponent<Transform>().localScale = new Vector2(2, 2);
                        height -= 400;
                    }
                    break;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SelectSlot(int id)
    {
        foreach(var button in SlotButtons)
        {
            button.GetComponent<Image>().color = new Color32(0x2b, 0x2b, 0x2b, 0xff);
        }
        SlotButtons[id-1].GetComponent<Image>().color = new Color32(0x20, 0x20, 0x20, 0xff);
        var slotCreated = SlotButtons[id-1].GetComponentsInChildren<TextMeshProUGUI>().First(x => x.name == "NameText").text != "Open";
        if(slotCreated)
        {
            LeftButton.GetComponentInChildren<TextMeshProUGUI>().text = "Play";
            LeftButton.SetActive(true);
            RightButton.GetComponentInChildren<TextMeshProUGUI>().text = "Delete";
            RightButton.SetActive(true);
        }
        else
        {
            LeftButton.GetComponentInChildren<TextMeshProUGUI>().text = "New Game";
            LeftButton.SetActive(true);
            RightButton.SetActive(false);
        }

        selectedSlot = id;
    }

    public void LeftButtonClick()
    {
        if(selectedSlot == 0)
        {
            return;
        }

        var slotCreated = SlotButtons[selectedSlot-1].GetComponentsInChildren<TextMeshProUGUI>().First(x => x.name == "NameText").text != "Open";
        if(!slotCreated)
        {
            Player.SaveFile = selectedSlot;
            SceneManager.LoadScene("NameScene");
            
        }
        else
        {
            Player.SaveFile = selectedSlot;
            Player.Name = null;
            SceneManager.LoadScene("MainScene");
        }
    }

    public void RightButtonClick()
    {
        if(selectedSlot == 0)
        {
            return;
        }

        File.Delete(Path.Combine(SaveLocation, $"save{selectedSlot}.json"));
        LeftButton.GetComponentInChildren<TextMeshProUGUI>().text = "New Game";
        LeftButton.SetActive(true);
        RightButton.GetComponentInChildren<TextMeshProUGUI>().text = "Delete";
        RightButton.SetActive(false);
        SlotButtons[selectedSlot-1].GetComponentsInChildren<TextMeshProUGUI>().First(x => x.name == "NameText").text = "Open";
        SlotButtons[selectedSlot-1].GetComponentsInChildren<TextMeshProUGUI>().First(x => x.name == "DayText").text = "";
    }

    public void BackButtonClick()
    {
        PreviousPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
