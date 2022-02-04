using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateClockText : NeedsClockUpdate
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void UpdateClock(ClockTime time)
    {
        GetComponent<Text>().text = $"{time.Hour.ToString("00")}:{time.Minute.ToString("00")}";
    }
}
