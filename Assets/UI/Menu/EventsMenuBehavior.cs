using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventsMenuBehavior : MonoBehaviour, IManualUpdate
{
    [SerializeField]
    private GameObject leftColumn;

    [SerializeField]
    private GameObject rightColumn;

    [SerializeField]
    private GameObject previousButton;

    [SerializeField]
    private GameObject nextButton;

    private bool started = false;

    private int currentPage = 1;
    private int pages;

    private Log eventLog;

    void Start()
    {
        if (!started)
        {
            started = true;
            eventLog = Resources.FindObjectsOfTypeAll<Log>().FirstOrDefault();
        }
    }

    public void MoveColumnDown()
    {
        currentPage++;
        SetPage();
        
        
    }

    public void MoveColumnUp()
    {
        currentPage--;
        SetPage();
    }

    public void SelectLine(int index)
    {
        var page = eventLog.LogList.OrderBy(x => x.Number).Skip((currentPage - 1) * 10).Take(10).ToList();
        if(index >= page.Count)
        {
            return;
        }
        LogItem item = page[index];
        StringBuilder logDisplay = new();
        logDisplay.Append($"Day {item.Time.Day}, {item.Time.Hour}:{item.Time.Minute}\n");
        logDisplay.Append(item.NoticeType).Append("\n\n");
        logDisplay.Append(item.Entry);
        rightColumn.GetComponent<TextMeshProUGUI>().text = logDisplay.ToString();
    }

    public void SetPage()
    {
        if (currentPage <= 1) 
        {
            currentPage = 1;
            previousButton.SetActive(false);
        }
        else
        {
            previousButton.SetActive(true);
        }
        if (currentPage >= pages)
        {
            currentPage = pages;
            nextButton.SetActive(false);
        }
        else
        {
            nextButton.SetActive(true);
        }

        List<LogItem> readable = eventLog.LogList.OrderBy(x => x.Number).Skip((currentPage - 1) * 10).Take(10).ToList();
        StringBuilder leftText = new();
        StringBuilder rightText = new();
        for (int i = 0; i < 10 && i < readable.Count; i++)
        {
            //leftText.Append(readable[i]).Append("\n");
            leftText.Append("Day ").Append(readable[i].Time.Day.ToString()).Append(" ")
                    .Append(readable[i].Time.Hour.ToString()).Append(":").Append(readable[i].Time.Hour.ToString());
            leftText.Append("  ").Append(readable[i].NoticeType).Append("\n");
        }

        leftColumn.GetComponent<TextMeshProUGUI>().text = leftText.ToString();
    }

    public void ManualUpdate()
    {
        if (!started)
        {
            Start();
        }
        pages = Math.Max(0, eventLog.LogList.Count - 1) / 10 + 1;
        SetPage();
    }
}
