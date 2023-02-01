using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityBGMusicUpdate : ActivityAction
{
    public MusicController musicController;
    public MusicListEnum audioClip;

    public void playClip()
    {
        musicController.currentAudio = audioClip;
        musicController.Play(audioClip);
        
    }
        

}



