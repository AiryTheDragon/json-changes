using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


public class IntroTextDisplay : MonoBehaviour
{
    public List<GameObject> TextList;

    public GameObject FinalPanel;

    private DateTime SlideStartTime = DateTime.Now;

    public int SecondsPerText;

    public int SecondsToFade;

    public int CurrentText;

    public AudioSource FirstSound;

    public AudioSource SecondSound;

    // Start is called before the first frame update
    void Start()
    {
        SlideStartTime = DateTime.Now;
        CurrentText = 0;
        FirstSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        double totalSeconds = ((DateTime.Now - SlideStartTime).TotalMilliseconds / 1000.0);
        int slide = (int)(totalSeconds / SecondsPerText);
        if(slide >= 1)
        {

            

            NextSlide();
        }
    }

    public void NextSlide()
    {

        if (CurrentText == 1)
        {
            FirstSound.Stop();
            SecondSound.Play();
        }

        SlideStartTime = DateTime.Now;
        CurrentText++;
        if(CurrentText < TextList.Count)
        {
            TextList[CurrentText - 1].SetActive(false);
            TextList[CurrentText].SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
            FinalPanel.SetActive(true);
        }
    }
}
