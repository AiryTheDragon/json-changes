using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuspicionSliderBehavior : MonoBehaviour, INeedsClockUpdate
{
    

    public GameObject Player;

    public GameObject Slider;


    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Clock").GetComponent<ClockBehavior>().NeedsClockUpdate.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateClock(ClockTime time)
    {
        Slider.GetComponent<Slider>().value = Player.GetComponent<Player>().Suspicion;
    }
}
