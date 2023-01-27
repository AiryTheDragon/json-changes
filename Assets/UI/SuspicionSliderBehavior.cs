using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuspicionSliderBehavior : MonoBehaviour, INeedsClockUpdate
{
    

    public GameObject Player;

    public GameObject Slider;

    public GameObject SusImage;

    private ClockTime lastSeenTime;
    private int lastSeenDiff = 0;

    private const float flashRate = 4.0f;


    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Clock").GetComponent<ClockBehavior>().NeedsClockUpdate.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(lastSeenTime is null)
        {
            return;
        }

        if(lastSeenDiff > 5)
        {
            var image = SusImage.GetComponent<Image>();
            image.color = new Color(1, 1, 1, 0);
            lastSeenTime = null;
        }
        else if(lastSeenDiff > 0)
        {
            var image = SusImage.GetComponent<Image>();
            image.color = new Color(1, 1, 1, Mathf.Abs(Mathf.Cos(Time.time * flashRate)));
        }
        else
        {
            var image = SusImage.GetComponent<Image>();
            image.color = Color.white;
        }
    }

    public void UpdateLastSeen(ClockTime time)
    {
        if(lastSeenTime is null || time > lastSeenTime)
        {
            lastSeenTime = new ClockTime(time);
            lastSeenDiff = GameObject.Find("Clock").GetComponent<ClockBehavior>().Time.subtractClockTime(lastSeenTime).Minute;
        }
    }

    public void UpdateClock(ClockTime time)
    {
        Slider.GetComponent<Slider>().value = (float)Player.GetComponent<Player>().Suspicion;
        if(lastSeenTime is not null)
        {
            lastSeenDiff = time.subtractClockTime(lastSeenTime).Minute;
        }
    }
}
