using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class YourBedBehavior : MonoBehaviour, IConfirmScript
{
  
    public GameObject confirmObject;
    public GameObject yesButton;
    public AudioClip _inBed;
    private AudioSource _source = null;
    public LoadSave loadSaveObject;
    public ClockBehavior clock;
    public MusicController musicController;
    
    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();
        if (_source == null)
        {
            Debug.Log("Audio Source is NULL");
        }
    }

    public void ConfirmAction()
    {
        loadSaveObject.LoadNextDayState();
        loadSaveObject.NextDayUpdates();
        loadSaveObject.CreateSaveData($"save{Player.SaveFile}");
        loadSaveObject.ExportSaveData($"save{Player.SaveFile}");
        musicController.IsReleased(); // resets music after sleeping
        
    }



    private void OnMouseUpAsButton()
    {
        var canvas = FindObjectOfType<MainCanvasBehavior>();
        if(canvas is not null)
        {
            if(canvas.AnyMenuOpen())
            {
                return;
            }
        }



        confirmObject.GetComponent<ConfirmMenu>().script = this;
        if (clock.timeToNextDay().Hour >= 0 && clock.timeToNextDay().Hour < 10)
        {

            confirmObject.GetComponent<ConfirmMenu>().UpdateText("Do you want to sleep until the next day?");
            _source.clip = _inBed;
            _source.Play();
            confirmObject.GetComponent<ConfirmMenu>().UpdateYesText("Yes!");
            confirmObject.GetComponent<ConfirmMenu>().UpdateNoText("No!");
            confirmObject.SetActive(true);
            yesButton.SetActive(true);
        }
        else
        {
            confirmObject.GetComponent<ConfirmMenu>().script = this;
            confirmObject.GetComponent<ConfirmMenu>().UpdateText("It is too bright to go to bed.");
            confirmObject.GetComponent<ConfirmMenu>().UpdateNoText("Ok.");
            _source.clip = _inBed;
            _source.Play();
            confirmObject.SetActive(true);
            yesButton.SetActive(false);
        }


    }



}
