using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    public List<LogItem> LogList = new();
    public int Counter = 1; // note Log numbers begin at 1
    public ClockBehavior clock;

    public void AddItem(string type, string note)
    {

        LogItem logItem = LogItem.CreateInstance<LogItem>();
        logItem.Number = Counter;
        logItem.Time = new ClockTime(clock.Time);
        logItem.NoticeType = type;
        logItem.Entry = note;

        LogList.Add(logItem);
        Counter++;
    }

    // note, log numbers began at 1
    public LogItem getItem(int number)
    {
        LogItem logItem = null;
        if (number - 1 < LogList.Count && number>0)
        {
            logItem = LogList[number - 1];
        }
        return logItem;
    }



}
