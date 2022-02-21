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
        Hour += hours;
        int days = Hour / 24;
        Hour = Hour % 24;
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
}
