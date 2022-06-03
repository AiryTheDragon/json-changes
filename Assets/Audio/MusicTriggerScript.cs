using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTriggerScript : MonoBehaviour
{
    public AudioClip audioClip;
    public BackgroundMusicScript backMusic;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //if (!backMusic.isPlaying())
            if (!backMusic.isPlaying() || !backMusic._source.clip.Equals(audioClip) )
            {
                backMusic.play(audioClip);
            }
        }
    }

}
