using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateClockText : MonoBehaviour, INeedsClockUpdate
{

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Clock").GetComponent<ClockBehavior>().NeedsClockUpdate.Add(this);
    }
    
    public void UpdateClock(ClockTime time)
    {
        GetComponent<Text>().text = $"{time.Hour.ToString("00")}:{time.Minute.ToString("00")}";
    }
}
