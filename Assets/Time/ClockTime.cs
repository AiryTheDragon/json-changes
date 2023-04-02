using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockTime
{
    public int Hour;

    public int Minute;

    public int Day;

    public int Week { get {
        return Day / 7;
    }}

    public int Year { get {
        return Day / 360;
    }}

    public int MonthOfYear { get {
        return (Day / 30) % 12;
    }}

    public int WeekOfYear { get {
        return (Day % 360) / 7;
    }}

    public int DayOfWeek { get {
        return (Day % 360) - ((Day % 360) / 7 * 7);
    }}

    public int DayOfMonth { get {
        return (Day % 360) - ((Day % 360) / 30 * 30);
    }}

    public int DayOfYear { get {
        return (Day % 360);
    }}

    public ClockTime() : this(0, 0, 0)
    {

    }

    public ClockTime(int day, int hour, int minute)
    {
        this.Hour = hour;
        this.Minute = minute;
        this.Day = day;
    }

    public ClockTime(int hour, int minute) : this (0, hour, minute)
    {
        
    }

    public ClockTime(ClockTime time) : this(time.Day, time.Hour, time.Minute)
    {

    }

    public void SetTime(int days, int hours, int minutes)
    {
        Day = days;
        Minute = minutes;
        Hour = hours;
    }

    public void AddMinutes(int minutes)
    {
        Minute += minutes;
        int hours = Minute / 60;
        Minute = Minute % 60;
        if (Minute<0)  // adjusts for negative minutes
        {
            Minute += 60;
            hours--;
        }
        
        Hour += hours;
        int days = Hour / 24;
        Hour = Hour % 24;
        if (Hour < 0) // adjusts for negative hours
        {
            Hour += 24;
            days--;
        }

        Day += days;

    }

    public void AddHours(int hours)
    {
        AddMinutes(hours * 60);
    }

    public void AddDays(int days)
    {
        AddHours(days * 24);
    }

 
    public ClockTime addClockTime(ClockTime timeToAdd)
    {
        ClockTime sumClockTime = new ClockTime(Day, Hour, Minute);
        sumClockTime.AddDays(timeToAdd.Day);
        sumClockTime.AddHours(timeToAdd.Hour);
        sumClockTime.AddMinutes(timeToAdd.Minute);

        return sumClockTime;

    }

    public ClockTime subtractClockTime(ClockTime timeToSubtract)
    {
        ClockTime differenceClockTime = new ClockTime(Day, Hour, Minute);
        differenceClockTime.AddDays(-1 * timeToSubtract.Day);
        differenceClockTime.AddHours(-1 * timeToSubtract.Hour);
        differenceClockTime.AddMinutes(-1 * timeToSubtract.Minute);

        return differenceClockTime;
    }

    public static bool operator >(ClockTime a, ClockTime b)
    {
        if(a.Year > b.Year)
        {
            return true;
        }
        else if(a.Day > b.Day)
        {
            return true;
        }
        else if(a.Hour > b.Hour)
        {
            return true;
        }
        else if(a.Minute > b.Minute)
        {
            return true;
        }
        return false;
    }

    public static bool operator <(ClockTime a, ClockTime b)
    {
        if(a.Year < b.Year)
        {
            return true;
        }
        else if(a.Day < b.Day)
        {
            return true;
        }
        else if(a.Hour < b.Hour)
        {
            return true;
        }
        else if(a.Minute < b.Minute)
        {
            return true;
        }
        return false;
    }

    public static bool operator ==(ClockTime a, ClockTime b)
    {
        if(a.GetHashCode() != b.GetHashCode())
        {
            return false;
        }
        return a.Year == b.Year && a.Day == b.Day && a.Hour == b.Hour && a.Minute == b.Minute;
    }

    public static bool operator !=(ClockTime a, ClockTime b)
    {
        if(a.GetHashCode() != b.GetHashCode())
        {
            return true;
        }
        return a.Year != b.Year || a.Day != b.Day || a.Hour != b.Hour || a.Minute != b.Minute;
    }

    public override bool Equals(object obj)
    {
        if (obj is ClockTime)
        {
            return this == (ClockTime)obj;
        }
        
        return false;
    }

    public override int GetHashCode()
    {
        return (Day, Hour, Minute).GetHashCode();
    }

}
