using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCameraBehavior : MonoBehaviour
{
    public double SpinTime;

    public int LeftTurnStop;

    public int RightTurnStop;

    public double WaitBetweenTurnsTime;

    public int SuspicionPerMinute;
    public DateTime StartTime = DateTime.Now;
    public bool TurningLeft;
    public bool Waiting;

    public bool seesPlayer;

    public Collider2D playerCollision;
  
    // Update is called once per frame
    void Update()
    {
        UpdateTurn();
        
    }


    private void UpdateTurn()
    {
        if(Waiting)
        {
            if(DateTime.Now - StartTime > TimeSpan.FromSeconds(WaitBetweenTurnsTime))
            {
                Waiting = false;
                StartTime = DateTime.Now;
            }
        }
        else
        {
            var transform = GetComponent<Transform>();
            var turningRadius = LeftTurnStop - RightTurnStop;
            if(TurningLeft)
            {
                var seconds = (DateTime.Now - StartTime).TotalMilliseconds / 1000.0;
                if(seconds > SpinTime)
                {
                    TurningLeft = false;
                    Waiting = true;
                    StartTime = DateTime.Now;
                }
                transform.localRotation = Quaternion.Euler(0, 0, (float)(RightTurnStop + seconds * turningRadius / SpinTime));
            }
            else
            {
                var seconds = (DateTime.Now - StartTime).TotalMilliseconds / 1000.0;
                if(seconds > SpinTime)
                {
                    TurningLeft = true;
                    Waiting = true;
                    StartTime = DateTime.Now;
                }
                transform.localRotation = Quaternion.Euler(0, 0, (float)(LeftTurnStop - seconds * turningRadius / SpinTime));
            }
        }
    }
}
