using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVScript : MonoBehaviour
{

    public bool isTVon;
    public float minSwitchTime = 1f;
    public float maxSwitchTime = 5f;

    private float timeRemaining;

    public SpriteRenderer reflectionImage;
    public AudioSource audioSounds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isTVon)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining<0)
            {
                switchStation();
                timeRemaining = getSwitchTime();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isTVon = true;
        timeRemaining = getSwitchTime();
        switchStation();
        audioSounds.Play();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTVon = false;
        reflectionImage.color = new Color(255, 255, 255, 0);
        audioSounds.Stop();
    }

    private float getSwitchTime()
    {
        return Random.Range(minSwitchTime, maxSwitchTime);
    }

    private void switchStation()
    {
        reflectionImage.color = Random.ColorHSV();
    }
}
