using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionSelectorBehavior : MonoBehaviour
{
    private int page = 1;

    private int totalPages;

    public List<GameObject> SeenActionDisplays;

    public List<Activity> ActivitiesToDisplay;

    public GameObject NextPageButton;

    public GameObject PreviousPageButton;

    public GameObject PageNumberDisplay;

    public GameObject LetterCreator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectActivity(Activity activity)
    {
        LetterCreator.GetComponent<LetterCreator>().ActivitySelected(activity);
    }

    public void SetActivities(List<Activity> activities)
    {
        this.ActivitiesToDisplay = activities;
        if(activities.Count <= SeenActionDisplays.Count)
        {
            totalPages = 1;
        }
        else
        {
            totalPages = ((activities.Count - 1) / SeenActionDisplays.Count) + 1;
        }
        SetPage(1);
    }

    public void SetPage(int page)
    {
        if(page > totalPages || page < 1)
        {
            return;
        }

        this.page = page;
        int i = 0;
        for(i = 0; i < SeenActionDisplays.Count && i + (page - 1) * SeenActionDisplays.Count < ActivitiesToDisplay.Count; i++)
        {
            SeenActionDisplays[i].GetComponent<SeenActionDisplayBehavior>().SetDisplayActivity(ActivitiesToDisplay[i + (page - 1) * SeenActionDisplays.Count]);
        }
        for(; i < SeenActionDisplays.Count; i++)
        {
            SeenActionDisplays[i].GetComponent<SeenActionDisplayBehavior>().SetDisplayActivity(null);
        }
        PageNumberDisplay.GetComponent<TextMeshProUGUI>().text = $"Page {this.page}/{this.totalPages}";
        if(HasNextPage())
        {
            NextPageButton.SetActive(true);
        }
        else
        {
            NextPageButton.SetActive(false);
        }

        if(HasPreviousPage())
        {
            PreviousPageButton.SetActive(true);
        }
        else
        {
            PreviousPageButton.SetActive(false);
        }
    }

    private bool HasNextPage()
    {
        if((ActivitiesToDisplay.Count - 1) / 2 > page) //TODO check this formula
        {
            return true;
        }
        return false;
    }

    public bool HasPreviousPage()
    {
        if(page - 1 > 0)
        {
            return true;
        }
        return false;
    }

    public void NextPage()
    {
        if(HasNextPage())
        {
            SetPage(page + 1);
        }
    }

    public void PreviousPage()
    {
        if(HasPreviousPage())
        {
            SetPage(page - 1);
        }
    }
}
