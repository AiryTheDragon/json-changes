using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenuBehavior : MonoBehaviour
{
    public List<GameObject> Pages;
    private int page;
    public GameObject turnLeftButton;
    public GameObject turnRightButton;

    // Start is called before the first frame update
    void Start()
    {
        page = 0;
        turnLeftButton.SetActive(false);
        turnRightButton.SetActive(true);
        for(int i = 1; i < Pages.Count; i++)
        {
            Pages[i].SetActive(false);
        }
        Pages[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnRight()
    {
        if(!turnLeftButton.activeInHierarchy)
        {
            turnLeftButton.SetActive(true);
        }
        if(page < Pages.Count - 1)
        {
            Pages[page].SetActive(false);
            Pages[++page].SetActive(true);
        }
        if(page == Pages.Count - 1)
        {
            turnRightButton.SetActive(false);
        }
    }

    public void TurnLeft()
    {
        if(!turnRightButton.activeInHierarchy)
        {
            turnRightButton.SetActive(true);
        }
        if(page > 0)
        {
            Pages[page].SetActive(false);
            Pages[--page].SetActive(true);
        }
        if(page == 0)
        {
            turnLeftButton.SetActive(false);
        }
    }
}
