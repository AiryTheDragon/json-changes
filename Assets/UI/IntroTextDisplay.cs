using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


public class IntroTextDisplay : MonoBehaviour
{
    public List<GameObject> TextList;

    private DateTime StartTime = DateTime.Now;

    public int SecondsPerText;

    public int SecondsToFade;

    public int CurrentText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        double totalSeconds = ((DateTime.Now - StartTime).TotalMilliseconds / 1000.0);
        int currentText = (int)(totalSeconds / SecondsPerText);
        if(CurrentText != currentText && currentText < TextList.Count)
        {
            TextList[currentText - 1].SetActive(false);
            TextList[currentText].SetActive(true);
        }
    }
}
