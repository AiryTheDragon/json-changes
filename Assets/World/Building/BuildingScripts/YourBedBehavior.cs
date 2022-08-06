using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class YourBedBehavior : MonoBehaviour, IConfirmScript
{
  
    public GameObject confirmObject;
    public AudioClip _inBed;
    private AudioSource _source = null;
    
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
        Debug.Log("In Confirm Action.");
    }



    private void OnMouseUpAsButton()
    {
        confirmObject.GetComponent<ConfirmMenu>().script = this;
        confirmObject.GetComponent<ConfirmMenu>().UpdateText("What is the answer?");
        _source.clip = _inBed;
        _source.Play();
        confirmObject.SetActive(true);
    }



}
