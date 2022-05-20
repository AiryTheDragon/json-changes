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

            if (!backMusic.isPlaying())
            {
                Debug.Log("Not playing background music.");
                backMusic.play(audioClip);
                // backMusic.playSneakingAround();
            }
        }
    }

}
