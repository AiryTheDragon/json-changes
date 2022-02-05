using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTriggerScript : MonoBehaviour
{
    public AudioSource audioSounds;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!audioSounds.isPlaying)
            {
                audioSounds.Play();
            }
        }
    }
}
