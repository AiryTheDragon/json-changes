using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMusicTriggerScript : MonoBehaviour
{
    public AudioSource audioSounds;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            
            audioSounds.Stop();

        }
    }

}
