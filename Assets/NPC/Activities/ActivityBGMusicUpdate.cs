using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityBGMusicUpdate : ActivityAction
{
    public BackgroundMusicScript backMusic;
    public AudioClip audioClip;

    public void playClip()
    {
        backMusic.play(audioClip);
    }


}



