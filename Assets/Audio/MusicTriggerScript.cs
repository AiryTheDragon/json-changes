using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTriggerScript : MonoBehaviour
{
    //public AudioClip audioClip;
    //public BackgroundMusicScript backMusic;
    public MusicZone zone;
    public MusicController controller;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            controller.EnterZone(zone);
            /*
            //if (!backMusic.isPlaying())
            if (!backMusic.isPlaying() || !backMusic._source.clip.Equals(audioClip) )
            {
                backMusic.play(audioClip);
            }
            */
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            controller.ExitZone(zone);
        }

    }

}
