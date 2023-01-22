using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityBGMusicUpdate : ActivityAction
{
    public MusicController musicController;
    public MusicListEnum audioClip;

    public void playClip()
    {
        Debug.LogError("Attempting to play an audioclip");
        musicController.currentAudio = audioClip;
        musicController.Play(audioClip);
        
    }
        

}



