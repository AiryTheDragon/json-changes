using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameMusicController : MonoBehaviour
{

    public EndGameMusicList musicList;
    // public MusicZone musicZone;
    public EndGameMusicListEnum musicListEnum;

    public EndGameMusicListEnum currentAudio;

    public AudioSource _source = null;
    private bool Playing = false;

    void Start()
    {
        /*
        _source = GetComponent<AudioSource>();
        if (_source == null)
        {
            Debug.Log("Audio Source is NULL");
        }
        */
    }

    public void Stop()
    {
        _source.Stop();
        Playing = false;
    }

    public void Play(EndGameMusicListEnum newAudio)
    {
        currentAudio = newAudio;
        _source.clip = musicList.GetClip(newAudio);
        _source.Play();
        Playing = true;
    }

    // starts the music playing going if it is not playing
    public void Play()
    {
        Play(currentAudio);
    }
    public bool IsPlaying()
    {
        return Playing;
    }

}

