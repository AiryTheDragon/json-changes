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

    public void TurnPageRight()
    {
        currentPage++;
        SetPage();
    }

    public void TurnPageLeft()
    {
        currentPage--;
        SetPage();
    }

    public void SetPage()
    {
        if (currentPage < 1) currentPage = 1;
        if (currentPage > pages) currentPage = pages;

        List<string> readable = eventLog.LogList.OrderBy(x => x.Number).Skip((currentPage - 1) * 10).Take(10).Select(x => x.Entry).ToList();
        StringBuilder leftText = new StringBuilder();
        StringBuilder rightText = new StringBuilder();
        for (int i = 0; i < 5 && i < readable.Count; i++)
        {
            leftText.Append(readable[i]).Append("\n");
        }
        if (readable.Count > 5)
        {
            for (int i = 5; i < readable.Count; i++)
            {
                rightText.Append(readable[i]).Append("\n");
            }
        }

        leftColumn.GetComponent<TextMeshProUGUI>().text = leftText.ToString();
        rightColumn.GetComponent<TextMeshProUGUI>().text = rightText.ToString();
    }

    public void ManualUpdate()
    {
        if (!started)
        {
            Start();
        }
        pages = Math.Max(0, (eventLog.LogList.Count - 1)) / 10 + 1;
        SetPage();
    }
}
