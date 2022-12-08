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

    // variables used in test 4 plodding
    public ClockTime Test4Time = new ClockTime(0, 3, 15);
    public bool Test4Status = false;
    public bool Monitor4 = false;
    public double SlowTime = 0.0;

    // variables used in test 5 click on NPC
    public ClockTime Test5Time = new ClockTime(0, 4, 15);
    public bool Test5Status = false;
    public bool Monitor5 = false;

    // variables used in test 6 pick up paper
    public ClockTime Test6Time = new ClockTime(0, 5, 35);
    public bool Test6Status = false;
    public bool Monitor6 = false;
    public GameObject paper;
    public GameObject pen;

    // variables used in test 7 approach desk
    public ClockTime Test7Time = new ClockTime(0, 7, 0);
    public bool Test7Status = false;
    public bool Monitor7 = false;

    // variables used in test 8 click revolt
    public ClockTime Test8Time = new ClockTime(0, 8, 0);
    public bool Test8Status = false;
    public bool Monitor8 = false;

    // variables used in test 9 click leave
    public ClockTime Test9Time = new ClockTime(0, 9, 0);
    public bool Test9Status = false;
    public bool Monitor9 = false;

    // variables used in test 10 returning to the desk
    public ClockTime Test10Time = new ClockTime(0, 10, 0);
    public bool Test10Status = false;
    public bool Monitor10 = false;

    // variables used in test 11 writing a letter
    public ClockTime Test11Time = new ClockTime(0, 12, 0);
    public bool Test11Status = false;
    public bool Monitor11 = false;
    public LetterCreator LetterMaker;

    // variables used in test 12 deliver letter
    public ClockTime Test12Time = new ClockTime(0, 13, 0);
    public bool Test12Status = false;
    public bool Monitor12 = false;
    public int Morale = 0;

    // variables used in test 10 returning to the desk
    public ClockTime Test13Time = new ClockTime(0, 14, 30);
    public bool Test13Status = false;
    public bool Monitor13 = false;

    // variables used in test 10 returning to the desk
    public ClockTime Test14Time = new ClockTime(0, 12, 0);
    public bool Test14Status = false;
    public bool Monitor14 = false;

    // The game clock
    public ClockBehavior GameClock;

    public GameObject TextBox;
    public TutorialTextChat TutorialText;


    public void Start()
    {
        ActivityTutorialBoxSpeak.boxText = TutorialText;
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
        if (Monitor5)
        {
            if (UpdateTest5())
            {
                GameClock.Time = new ClockTime(Test5Time);
                Monitor5 = false;

            }
        }
        if (Monitor6)
        {
            if (UpdateTest6())
            {
                GameClock.Time = new ClockTime(Test6Time);
                Monitor6 = false;

            }
        }
        if (Monitor7)
        {
            if (UpdateTest7())
            {
                GameClock.Time = new ClockTime(Test7Time);
                Monitor7 = false;
                TextBox.SetActive(true);

            }
        }
        if (Monitor8)
        {
            if (UpdateTest8())
            {
                GameClock.Time = new ClockTime(Test8Time);
                Monitor8 = false;

            }
        }
        if (Monitor9)
        {
            if (UpdateTest9())
            {
                GameClock.Time = new ClockTime(Test9Time);
                Monitor9 = false;
                TextBox.SetActive(false);

            }
        }
        if (Monitor10)
        {
            if (UpdateTest10())
            {
                GameClock.Time = new ClockTime(Test10Time);
                Monitor10 = false;
                TextBox.SetActive(true);

            }
        }
        if (Monitor11)
        {
            if (UpdateTest11())
            {
                GameClock.Time = new ClockTime(Test11Time);
                Monitor11 = false;
                TextBox.SetActive(false);

            }
        }

        if (Monitor12)
        {
            if (UpdateTest12())
            {
                GameClock.Time = new ClockTime(Test12Time);
                Monitor12 = false;
            }
        }

        if (Monitor13)
        {
            if (UpdateTest13())
            {
                GameClock.Time = new ClockTime(Test13Time);
                Monitor13 = false;
                TextBox.SetActive(true);
            }
        }

        if (Monitor14)
        {
            if (UpdateTest14())
            {
                GameClock.Time = new ClockTime(Test14Time);
                Monitor14 = false;
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
        Test3Status = false;
        Test4Status = false;
        Test5Status = false;
        Test6Status = false;
        Test7Status = false;
        Test8Status = false;
        Test9Status = false;
        Test10Status = false;
        Test11Status = false;
        Test12Status = false;
        Test13Status = false;
        Test14Status = false;
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

    public void MonitorTest5(bool value)
    {
        Monitor5 = value;
    }
    public void MonitorTest6(bool value)
    {
        Monitor6 = value;
    }

    public void MonitorTest7(bool value)
    {
        Monitor7 = value;
    }

    public void MonitorTest8(bool value)
    {
        Monitor8 = value;
    }

    public void MonitorTest9(bool value)
    {
        Monitor9 = value;
    }

    public void MonitorTest10(bool value)
    {
        Monitor10 = value;
    }

    public void MonitorTest11(bool value)
    {
        Monitor11 = value;
    }

    public void MonitorTest12(bool value)
    {
        Monitor12 = value;
    }

    public void MonitorTest13(bool value)
    {
        Monitor13 = value;
    }

    public void MonitorTest14(bool value)
    {
        Monitor14 = value;
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
        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && Input.GetAxis("Fire1") > 0)
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

    public bool UpdateTest5()
    {
        return Test5Status;
    }

    public bool UpdateTest6()
    {
        if (!paper.activeSelf && !pen.activeSelf)
        {
            Test6Status = true;
        }
        return Test6Status;
    }

    public bool UpdateTest7()
    {
        return Test7Status;
    }

    public bool UpdateTest8()
    {
        return Test8Status;
    }

    public bool UpdateTest9()
    {
        return Test9Status;
    }

    public bool UpdateTest10()
    {
        return Test10Status;
    }
    public bool UpdateTest11()
    {
        return Test11Status;
    }
    public bool UpdateTest12()
    {
        if (GetComponent<NPCBehavior>().ManipulationLevel != Morale)
        {
            Morale = GetComponent<NPCBehavior>().ManipulationLevel;
            Test12Status = true;
        }
        return Test12Status;
    }

    public bool UpdateTest13()
    {
        if (Input.GetKey(KeyCode.I) || Input.GetKey(KeyCode.Escape))
        {
            Test13Status = true;
        }
        return Test13Status;
    }

    public bool UpdateTest14()
    {
        return Test14Status;
    }
    public void EnterDesk(bool value)
    {
        if (Monitor7)
        {
            Test7Status = value;
        }

        if (Monitor10)
        {
            Test10Status = value;
        }

    }

    public void ClickRevoltLetter()
    {
        if (Monitor8)
        {
            Test8Status = true;
        }
    }

    public void ClickLeave()
    {
        if (Monitor9)
        {
            Test9Status = true;
        }
    }

    public void ClickCreateLetter()
    {
        if (Monitor11)
        {
            Test11Status = true;
            LetterMaker.CreateLetter();
        }
    }

    public void updateMonitorTests(ActivityTutorialMonitor list)
    {
        Monitor1 = list.Test1;
        Monitor2 = list.Test2;
        Monitor3 = list.Test3;
        Monitor4 = list.Test4;
        Monitor5 = list.Test5;
        Monitor6 = list.Test6;
        Monitor7 = list.Test7;
        Monitor8 = list.Test8;
        Monitor9 = list.Test9;
        Monitor10 = list.Test10;
        Monitor11 = list.Test11;
        if (!Monitor12 && list.Test12) // if starting to monitor manipulation level, set initial value
        {
            Morale = GetComponent<NPCBehavior>().ManipulationLevel;
        }
        Monitor12 = list.Test12;
        Monitor13 = list.Test13;
        Monitor14 = list.Test14;

    }

    void OnMouseUpAsButton()
    {
        if (Monitor5)
        {
            Test5Status = true;
        }
    }


}
