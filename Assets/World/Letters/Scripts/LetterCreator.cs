using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class LetterCreator : MonoBehaviour
{
    public List<Person> People;

    public GameObject BannedActivitiesObject;
    
    public GameObject CurrentLetter;

    public GameObject BlackmailLetter;

    public GameObject CurrentNotebook;

    public GameObject PersonSelector;

    public GameObject ActivitySelector;

    public Person SelectedPerson;

    public Activity SelectedActivity;

    public GameObject Player;

    public GameObject Creator;

    public TextMeshProUGUI InkAmountText;

    public TextMeshProUGUI PaperAmountText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPersonSelector()
    {
        if(CurrentNotebook != null)
        {
            CurrentNotebook.SetActive(false);
        }
        CurrentNotebook = PersonSelector;
        PersonSelector.SetActive(true);
    }

    public void PersonSelected(Person person)
    {
        PersonSelector.SetActive(false);
        SelectedPerson = person;
        if(CurrentLetter.name == "BlackmailLetter")
        {
            CurrentLetter.GetComponent<BlackmailLetterBehavior>().SelectPerson(person);
            SelectedActivity = CurrentLetter.GetComponent<BlackmailLetterBehavior>().SelectedActivity;
        }
    }

    public void OpenActivitySelector()
    {
        if(CurrentNotebook != null)
        {
            CurrentNotebook.SetActive(false);
        }
        if(SelectedPerson != null)
        {
            CurrentNotebook = ActivitySelector;
            ActivitySelector.GetComponent<ActionSelectorBehavior>().SetActivities(SelectedPerson.SeenActivities.Where(
                    x => BannedActivitiesObject.GetComponent<BannedActivitiesBehavior>().BannedActivities.ContainsKey(x)
                ).ToList());
            ActivitySelector.SetActive(true);
        }
    }

    public void ActivitySelected(Activity activity)
    {
        ActivitySelector.SetActive(false);
        SelectedActivity = activity;
        if(CurrentLetter.name == "BlackmailLetter")
        {
            CurrentLetter.GetComponent<BlackmailLetterBehavior>().SelectActivity(activity);
        }
    }

    public void CreateLetter()
    {
        if(CurrentLetter.name == "BlackmailLetter")
        {
            if(SelectedActivity != null && SelectedPerson != null)
            {
                Letter letter = new Letter();
                letter.Recieving = SelectedPerson;
                letter.ManipulationLevelIncrease = BannedActivitiesObject.GetComponent<BannedActivitiesBehavior>().BannedActivities[SelectedActivity];
                var player = Player.GetComponent<Player>();
                player.invScript.AddLetter(letter);
                LeaveCreator();
            }
        }
    }

    public void LeaveCreator()
    {
        Creator.SetActive(false);
        if(CurrentNotebook != null)
        {
            CurrentNotebook.SetActive(false);
            CurrentNotebook = null;
        }
        if(CurrentLetter != null)
        {
            CurrentLetter.SetActive(false);
            CurrentLetter = null;
        }
    }

    public void EnterCreator()
    {
        CurrentLetter = BlackmailLetter;
        CurrentLetter.SetActive(true);
        BlackmailLetter.GetComponent<BlackmailLetterBehavior>().SelectPerson(null);
        People = Player.GetComponent<Player>().PeopleKnown;
        InkAmountText.text = $"{Player.GetComponent<Player>().invScript.Pens}";
        PaperAmountText.text = $"{Player.GetComponent<Player>().invScript.Paper}";
        Creator.SetActive(true);
    }
}
