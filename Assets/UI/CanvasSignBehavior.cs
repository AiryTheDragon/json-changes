using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using TMPro;

public class CanvasSignBehavior : MonoBehaviour
{
    public GameObject thisSign;
    public GameObject thisText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Close()
    {
        Debug.Log("Setting components to false");
        thisSign.SetActive(false);
        thisText.SetActive(false);

    }

    public void Open(string text)
    {
        thisSign.SetActive(true);
        thisText.SetActive(true);
        thisText.GetComponent<TextMeshProUGUI>().text = text;

        Debug.Log("Setting components to true");

    }

}
