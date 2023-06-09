using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class TutorialBedBehavior : MonoBehaviour, IConfirmScript
{
  
    public GameObject confirmObject;
    public GameObject yesButton;
    public AudioClip _inBed;
    private AudioSource _source = null;
    public LoadSave loadSaveObject;
    public ClockBehavior clock;
    public static bool tutorialComplete = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();
        if (_source == null)
        {
            Debug.Log("Audio Source is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConfirmAction()
    {
        tutorialComplete = true;
        SceneManager.LoadScene("MainScene");
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
        if (clock.timeToNextDay().Hour >= 7 && clock.timeToNextDay().Hour < 9)
        {

            confirmObject.GetComponent<ConfirmMenu>().UpdateText("Do you want to wake up?");
            confirmObject.GetComponent<ConfirmMenu>().UpdateYesText("Yes!");
            confirmObject.GetComponent<ConfirmMenu>().UpdateNoText("No!");
            _source.clip = _inBed;
            _source.Play();
            confirmObject.SetActive(true);
            yesButton.SetActive(true);
        }
        else
        {
            confirmObject.GetComponent<ConfirmMenu>().script = this;
            confirmObject.GetComponent<ConfirmMenu>().UpdateText("You are unable to wake up.");
            confirmObject.GetComponent<ConfirmMenu>().UpdateNoText("Ok!");
            _source.clip = _inBed;
            _source.Play();
            confirmObject.SetActive(true);
            yesButton.SetActive(false);
        }


    }



}
