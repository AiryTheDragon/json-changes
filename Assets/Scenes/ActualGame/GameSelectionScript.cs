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
