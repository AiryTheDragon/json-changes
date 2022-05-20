using UnityEngine;
using System.Linq;
using System;
using System.Text;
using System.Collections.Generic;
using TMPro;

public class BannedActivitiesMenuBehavior : MonoBehaviour, IManualUpdate
{
    [SerializeField]
    private GameObject leftColumn;

    [SerializeField]
    private GameObject rightColumn;

    private BannedActivitiesBehavior bannedActivities;

    private List<string> bannedList;

    private bool started = false;

    private int currentPage = 1;
    private int pages;

    void Start()
    {
        if(!started)
        {
            started = true;
            bannedActivities = Resources.FindObjectsOfTypeAll<BannedActivitiesBehavior>().First();
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
        if(currentPage < 1) currentPage = 1;
        if(currentPage > pages) currentPage = pages;

        List<string> readable = bannedList.Skip((currentPage - 1) * 10).Take(10).ToList();
        StringBuilder leftText = new StringBuilder();
        StringBuilder rightText = new StringBuilder();
        for(int i = 0; i < 5 && i < readable.Count; i++)
        {
            leftText.Append(readable[i]).Append("\n");
        }
        if(readable.Count > 5)
        {
            for(int i = 5; i < readable.Count; i++)
            {
                rightText.Append(readable[i]).Append("\n");
            }
        }

        leftColumn.GetComponent<TextMeshProUGUI>().text = leftText.ToString();
        rightColumn.GetComponent<TextMeshProUGUI>().text = rightText.ToString();
    }

    public void ManualUpdate()
    {
        if(!started)
        {
            Start();
        }
        bannedList = bannedActivities.BannedActivities.Select(x => x.Name).ToList();
        pages = Math.Max(0, (bannedList.Count - 1)) / 10 + 1;
        SetPage();
    }
}