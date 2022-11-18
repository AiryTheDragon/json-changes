using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTests : MonoBehaviour
{
    // time threshhold for all tests
    public double timeThreshhold = 1.0;

    // variables used in test 1 - moving
    public ClockTime Test1Time = new ClockTime(0, 1, 45);
    public bool Test1Status = false;
    public bool Monitor1 = false;
    public double LeftTime = 0.0;
    public double RightTime = 0.0;
    public double UpTime = 0.0;
    public double DownTime = 0.0;

    // variables used in test 2 - lights
    public ClockTime Test2Time = new ClockTime(0, 2, 15);
    public bool Test2Status = false;
    public bool Monitor2 = false;
    public LampBehavior lamp1;
    public LampBehavior lamp2;

    // variables used in test 3 running
    public ClockTime Test3Time = new ClockTime(0, 2, 45);
    public bool Test3Status = false;
    public bool Monitor3 = false;
    public double SpeedTime = 0.0;

    // variables used in test 4 unassuming
    public ClockTime Test4Time = new ClockTime(0, 3, 15);
    public bool Test4Status = false;
    public bool Monitor4 = false;
    public double SlowTime = 0.0;

    // The game clock
    public ClockBehavior GameClock;

    public void Start()
    {
        
    }

    public void Update()
    {
        if (Monitor1)
        {
            if (UpdateTest1())
            {
                GameClock.Time = new ClockTime(Test1Time);
                Monitor1 = false;
            };
        }
        if (Monitor2)
        {
            if (UpdateTest2())
            {
                GameClock.Time = new ClockTime(Test2Time);
                Monitor2 = false;
            }
        }
        if(Monitor3)
        {
            if (UpdateTest3())
            {
                GameClock.Time = new ClockTime(Test3Time);
                Monitor3 = false;
            }
        }
        if(Monitor4)
        {
            if (UpdateTest4())
            {
                GameClock.Time = new ClockTime(Test4Time);
                Monitor4 = false;
            }
        }
    }

    public void ResetTests()
    {
        Test1Status = false;
        LeftTime = 0.0;
        RightTime = 0.0;
        UpTime = 0.0;
        DownTime = 0.0;
        Test2Status = false;
        SpeedTime = 0.0;
        SlowTime = 0.0;


}

    public void MonitorTest1(bool value)
    {
        Monitor1 = value;
    }

    public void MonitorTest2(bool value)
    {
        Monitor2 = value;
    }
    public void MonitorTest3(bool value)
    {
        Monitor3 = value;
    }
    public void MonitorTest4(bool value)
    {
        Monitor4 = value;
    }

    public bool UpdateTest1()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            RightTime += Time.deltaTime;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            LeftTime += Time.deltaTime;
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            UpTime += Time.deltaTime;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            DownTime += Time.deltaTime;
        }

        if (RightTime>timeThreshhold && LeftTime>timeThreshhold && UpTime>timeThreshhold && DownTime>timeThreshhold)
        {
            Test1Status = true;
        }

        return Test1Status;
    }

    public bool UpdateTest2()
    {
        if (lamp1.on && lamp2.on)
        {
            Test2Status = true;
        }
        return Test2Status;
    }

    public bool UpdateTest3()
    {
        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && Input.GetAxis("Fire1")>0)
        {
            SpeedTime += Time.deltaTime;
        }

        if (SpeedTime>timeThreshhold)
        {
            Test3Status = true;
        }

        return Test3Status;
    }

    public bool UpdateTest4()
    {
        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && Input.GetAxis("Fire2") > 0)
        {
            SlowTime += Time.deltaTime;
        }

        if (SlowTime > timeThreshhold)
        {
            Test4Status = true;
        }

        return Test4Status;
    }


    public void updateMonitorTests(ActivityTutorialMonitor list)
    {
        Monitor1 = list.Test1;
        Monitor2 = list.Test2;
        Monitor3 = list.Test3;
        Monitor4 = list.Test4;
    }


}
