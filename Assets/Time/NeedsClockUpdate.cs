using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NeedsClockUpdate : MonoBehaviour
{
    public abstract void UpdateClock(ClockTime time);
}
