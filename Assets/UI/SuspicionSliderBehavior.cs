using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuspicionSliderBehavior : MonoBehaviour, INeedsClockUpdate
{
    

    public GameObject Player;

    public GameObject Slider;

    private bool initialized;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!initialized)
        {
            initialized = true;
            ClockBehavior.NeedsClockUpdate.Add(this);
        }
    }

    public void UpdateClock(ClockTime time)
    {
        Slider.GetComponent<Slider>().value = Player.GetComponent<Player>().Suspicion;
    }
}
