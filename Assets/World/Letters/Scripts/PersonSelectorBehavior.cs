using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PersonSelectorBehavior : MonoBehaviour
{
    private int currentPage = 1;

    private int pages = 1;

    private List<Person> People = new();

    public List<GameObject> PersonDisplays;

    public GameObject NextPageButton;

    public GameObject PreviousPageButton;

    public GameObject PageNumberDisplay;

    public GameObject LetterCreator;


    public void SelectPerson(Person person)
    {
        LetterCreator.GetComponent<LetterCreator>().PersonSelected(person);
    }

    public void SetPeople(List<Person> people)
    {
        this.People = people;
        if(people.Count <= PersonDisplays.Count)
        {
            pages = 1;
        }
        else
        {
            pages = ((people.Count - 1) / PersonDisplays.Count) + 1;
        }
        if (currentPage >= 1 && currentPage <= pages)
        {
            SetPage(currentPage);
        }
        else
        {
            SetPage(1);
        }
        ;
    }

    public void SetPage(int page)
    {
        if(page > pages || page < 1)
        {
            return;
        }
        this.currentPage = page;
        int i = 0;
        for(; i < PersonDisplays.Count && i + (page - 1) * PersonDisplays.Count < People.Count; i++)
        {
            PersonDisplays[i].GetComponent<PersonDisplayBehavior>().SetPerson(People[i + (page - 1) * PersonDisplays.Count]);
        }

        for(; i < PersonDisplays.Count; i++)
        {
            PersonDisplays[i].GetComponent<PersonDisplayBehavior>().SetPerson(null);
        }

        PageNumberDisplay.GetComponent<TextMeshProUGUI>().text = $"Page {this.currentPage}/{this.pages}";
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
        if((People.Count + 1)/ 2 > currentPage)
        {
            return true;
        }
        return false;
    }

    public bool HasPreviousPage()
    {
        if(currentPage - 1 > 0)
        {
            return true;
        }
        return false;
    }

    public void NextPage()
    {
        if(HasNextPage())
        {
            SetPage(currentPage + 1);
        }
    }

    public void PreviousPage()
    {
        if(HasPreviousPage())
        {
            SetPage(currentPage - 1);
        }
    }
}
