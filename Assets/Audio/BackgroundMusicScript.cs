using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicScript : MonoBehaviour
{
    public AudioClip _sneakingAround;
    public AudioClip _tension;

    public AudioSource _source = null;
    private bool Playing = false;

    void Start()
    {
        _source = GetComponent<AudioSource>();
        if (_source == null)
        {
            Debug.Log("Audio Source is NULL");
        }
        else
        {
            _source.clip = _sneakingAround;
        }
    }

    public void playSneakingAround()
    {
        Debug.Log("Starting Sneaking Around");
        _source.clip = _sneakingAround;
        _source.Play();
        Playing = true;
    }

    public void playTension()
    {
        _source.clip = _tension;
        _source.Play();
        Playing = true;
    }

    public void stop()
    {
        _source.Stop();
        Playing = false;
    }

    public void play(AudioClip audioClip)
    {
        _source.clip = audioClip;
        _source.Play();
        Playing = true;
    }
    public bool isPlaying()
    {
        return Playing;
    }
}

