using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    public MusicList musicList;
    // public MusicZone musicZone;
    public MusicListEnum musicListEnum;

    public bool inBoomBoxZone = false;
    public bool isBoomBoxOn = false;
    public bool isChased = false;
    public bool isCaught = false;
    public bool isHighSus = false;

    public MusicZone currentZone;
    public MusicListEnum currentAudio;

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
            _source.clip = musicList.Home;
        }
    }

    public void EnterZone(MusicZone zone)
    {
        Debug.Log("New zone entered!");
        MusicListEnum nextAudio = MusicListEnum.Other;

        if (currentZone != zone)
        {
            currentZone = zone;
            nextAudio = GetMusic();
            
            if (nextAudio!=currentAudio)
            {
                currentAudio = nextAudio;
                Play(currentAudio);
            }
        }
    }

    public void ExitZone(MusicZone zone)
    {
        if (zone==MusicZone.BoomBox)
        {
            EnterZone(MusicZone.Outside);
        }
    }

    public void IsChased()
    {
        isChased = true;
        isCaught = false;

        MusicListEnum nextAudio = MusicListEnum.Chase;

        if (isHighSus)
        {
            nextAudio = MusicListEnum.HighSusChase;
        }

        if (currentAudio!=nextAudio)
        {
            currentAudio = nextAudio;
            Play(currentAudio);
        }
    }

    public void IsCaught()
    {
        isChased = false;
        isCaught = true;

        MusicListEnum nextAudio = MusicListEnum.Caught;

        if (isHighSus)
        {
            nextAudio = MusicListEnum.HighSusCaught;
        }

        if (currentAudio != nextAudio)
        {
            currentAudio = nextAudio;
            Play(currentAudio);
        }
    }

    public void IsReleased()
    {
        isChased = false;
        isCaught = false;

        MusicZone thisZone = currentZone;
        currentZone = MusicZone.Other;

        EnterZone(thisZone);
        
    }

    public void IsHighSus()
    {
        if (!isHighSus)
        {
            isHighSus = true;
            if (currentAudio == MusicListEnum.Chase)
            { 
                currentAudio = MusicListEnum.HighSusChase;
                Play(currentAudio);
            }
            else if (currentAudio == MusicListEnum.Caught)
            {
                currentAudio = MusicListEnum.HighSusCaught;
                Play(currentAudio);
            }
            else if (currentAudio != MusicListEnum.HighSusOutside)
            {
                currentAudio = MusicListEnum.HighSusOutside;
                Play(currentAudio);
            }
        }
    }

    public void IsNotHighSus()
    {
        if (isHighSus)
        {
            isHighSus = false;

            MusicZone thisZone = currentZone;
            currentZone = MusicZone.Other;

            EnterZone(thisZone);
        }
    }

    public void TurnOnBoomBox()
    {
        isBoomBoxOn = true;
        if ((currentAudio==MusicListEnum.Outside || currentAudio==MusicListEnum.HighSusOutside) && currentZone==MusicZone.BoomBox)
        {
            currentAudio = MusicListEnum.NoSound;
            Play(currentAudio);
        }
    }
    public void TurnOffBoomBox()
    {
        isBoomBoxOn = false;
        if (currentAudio == MusicListEnum.NoSound && currentZone == MusicZone.BoomBox)
        {
            if (isHighSus)
            {
                currentAudio = MusicListEnum.HighSusOutside;
            }
            else
            {
                currentAudio = MusicListEnum.Outside;
            }
            Play(currentAudio);
        }
    }

    public void Stop()
    {
        _source.Stop();
        Playing = false;
    }

    public void Play(MusicListEnum newAudio)
    {
        currentAudio = newAudio;
        _source.clip = musicList.GetClip(newAudio);
        _source.Play();
        Playing = true;
    }

    // starts the music playing going if it is not playing
    public void Play()
    {
        Play(GetMusic());
    }
    public bool IsPlaying()
    {
        return Playing;
    }

    public MusicListEnum GetMusic()
    {
        MusicListEnum nextAudio = MusicListEnum.NoSound;
        if (!isCaught && !isChased && !isHighSus)
        {
            switch (currentZone)
            {

                case MusicZone.Home:
                    nextAudio = MusicListEnum.Home;
                    break;
                case MusicZone.Outside:
                    nextAudio = MusicListEnum.Outside;
                    break;
                case MusicZone.BoomBox:
                    if (isBoomBoxOn)
                    {
                        nextAudio = MusicListEnum.NoSound;
                    }
                    else
                    {
                        nextAudio = MusicListEnum.Outside;
                    }
                    break;
                case MusicZone.Jail:
                    nextAudio = MusicListEnum.Jail;
                    break;
                case MusicZone.JailCell:
                    nextAudio = MusicListEnum.JailCell;
                    break;
                case MusicZone.PrintingPress:
                    nextAudio = MusicListEnum.PrintingPress;
                    break;
                case MusicZone.Admin:
                    nextAudio = MusicListEnum.Admin;
                    break;
                case MusicZone.Other:
                    nextAudio = MusicListEnum.NoSound;
                    break;
            }
        }
        else if (isHighSus)
        {
            if (isCaught)
            {
                nextAudio = MusicListEnum.HighSusCaught;
            }
            else if (isChased)
            {
                nextAudio = MusicListEnum.HighSusChase;
            }
            else if (currentZone == MusicZone.BoomBox && isBoomBoxOn)
            {
                nextAudio = MusicListEnum.NoSound;
            }
            else
            {
                nextAudio = MusicListEnum.HighSusOutside;
            }
        }
        else if (isCaught)
        {
            nextAudio = MusicListEnum.Caught;
        }
        else if (isChased)
        {
            nextAudio = MusicListEnum.Chase;
        }
        return nextAudio;
    }


}

