using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    public List<LogItem> LogList = new List<LogItem>();
    public int Counter = 1; // note Log numbers begin at 1
    public ClockBehavior clock;

    public void addItem(string type, string note)
    {

        LogItem logItem = LogItem.CreateInstance<LogItem>();
        logItem.Number = Counter;
        logItem.Time = clock.Time;
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
