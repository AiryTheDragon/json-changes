using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PersonSelectorBehavior : MonoBehaviour
{
    private int page = 1;

    private int totalPages;

    private List<Person> People = new List<Person>();

    public List<GameObject> PersonDisplays;

    public GameObject NextPageButton;

    public GameObject PreviousPageButton;

    public GameObject PageNumberDisplay;

    public GameObject LetterCreator;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SelectPerson(Person person)
    {
        LetterCreator.GetComponent<LetterCreator>().PersonSelected(person);
    }

    public void SetPeople(List<Person> people)
    {
        this.People = people;
        if(people.Count <= PersonDisplays.Count)
        {
            totalPages = 1;
        }
        else
        {
            totalPages = ((people.Count - 1) / PersonDisplays.Count) + 1;
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
        for(; i < PersonDisplays.Count && i + (page - 1) * PersonDisplays.Count < People.Count; i++)
        {
            PersonDisplays[i].GetComponent<PersonDisplayBehavior>().SetPerson(People[i + (page - 1) * PersonDisplays.Count]);
        }

        for(; i < PersonDisplays.Count; i++)
        {
            PersonDisplays[i].GetComponent<PersonDisplayBehavior>().SetPerson(null);
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
        if((People.Count - 1) / 2 > page)
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
