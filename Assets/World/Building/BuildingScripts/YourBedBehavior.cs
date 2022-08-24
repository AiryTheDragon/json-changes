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
    public LoadSave loadSaveObject;
    
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
        Debug.LogWarning("The next day state is being saved.\nWe don't want this in the launched game.");
        loadSaveObject.SaveNextDayState();
        loadSaveObject.LoadNextDayState();
        loadSaveObject.NextDayUpdates();
        //loadSaveObject.CreateSaveData();
    }



    private void OnMouseUpAsButton()
    {
        confirmObject.GetComponent<ConfirmMenu>().script = this;
        confirmObject.GetComponent<ConfirmMenu>().UpdateText("Do you want to sleep until the next day?");
        _source.clip = _inBed;
        _source.Play();
        confirmObject.SetActive(true);
    }



}
