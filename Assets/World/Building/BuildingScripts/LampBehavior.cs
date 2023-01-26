using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering.Universal;

public class LampBehavior : MonoBehaviour, IClickable
{
    [SerializeField] private AudioClip _turnLightOn = null;
    [SerializeField] private AudioClip _turnLightOff = null;

    private AudioSource _source = null;

    public GameObject onDoorObject;
    public GameObject offDoorObject;

    public bool on = false;

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

    public void TurnOn()
    {
        if (!on)
        {
            offDoorObject.GetComponent<SpriteRenderer>().enabled = false;
            // offDoorObject.GetComponent<UnityEngine.Rendering.Universal.ShadowCaster2D>().castsShadows = false;
            onDoorObject.GetComponent<SpriteRenderer>().enabled = true;
            onDoorObject.GetComponent<Light2D>().enabled = true;
            // onDoorObject.GetComponent<UnityEngine.Rendering.Universal.ShadowCaster2D>().castsShadows = true;

            if (this.CompareTag("BoomBox"))
            {
                Debug.LogWarning("You clicked the Boombox!");
                // get musicController to be able to adjust background music
                MusicController musicController = musicController = GameObject.FindGameObjectWithTag("MusicController").GetComponent<MusicController>();
                musicController.TurnOnBoomBox();
                
            }


            _source.clip = _turnLightOn;
            _source.Play();
            on = true;
        }
    }
    // Used for loading/restoring
    public void TurnOnQuiet()
    {
        if (!on)
        {
            offDoorObject.GetComponent<SpriteRenderer>().enabled = false;
            // offDoorObject.GetComponent<UnityEngine.Rendering.Universal.ShadowCaster2D>().castsShadows = false;
            onDoorObject.GetComponent<SpriteRenderer>().enabled = true;
            onDoorObject.GetComponent<Light2D>().enabled = true;
            // onDoorObject.GetComponent<UnityEngine.Rendering.Universal.ShadowCaster2D>().castsShadows = true;

            on = true;
        }
    }
    public void TurnOff()
    {
        if (on)
        {
            onDoorObject.GetComponent<SpriteRenderer>().enabled = false;
            onDoorObject.GetComponent<Light2D>().enabled = false;
            // onDoorObject.GetComponent<UnityEngine.Rendering.Universal.ShadowCaster2D>().castsShadows = false;
            offDoorObject.GetComponent<SpriteRenderer>().enabled = true;
            // offDoorObject.GetComponent<UnityEngine.Rendering.Universal.ShadowCaster2D>().castsShadows = true;

            if (this.CompareTag("BoomBox"))
            {
                Debug.LogWarning("You clicked the Boombox!");
                
                // get musicController to be able to adjust background music
                MusicController musicController = musicController = GameObject.FindGameObjectWithTag("MusicController").GetComponent<MusicController>();
                musicController.TurnOffBoomBox();
                
            }



            _source.clip = _turnLightOff;
            _source.Play();
            on = false;
        }
    }
    // used for loading/restoring
    public void TurnOffQuiet()
    {
        if (on)
        {
            onDoorObject.GetComponent<SpriteRenderer>().enabled = false;
            onDoorObject.GetComponent<Light2D>().enabled = false;
            // onDoorObject.GetComponent<UnityEngine.Rendering.Universal.ShadowCaster2D>().castsShadows = false;
            offDoorObject.GetComponent<SpriteRenderer>().enabled = true;
            // offDoorObject.GetComponent<UnityEngine.Rendering.Universal.ShadowCaster2D>().castsShadows = true;
            on = false;
        }
    }

    public void OnMouseUpAsButton()
    {
        Toggle();
        Debug.Log("Light is toggled.");
    }

    public void Toggle()
    {
        if (on)
        {
            TurnOff();
        }
         else 
        {
            TurnOn();
        }
    }

}
