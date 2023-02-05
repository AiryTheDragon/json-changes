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

    public GameObject GiftLetter;

    public GameObject RevolutionLetter;

    public TextMeshProUGUI RevolutionLetterText;

    public GameObject CurrentNotebook;

    public GameObject PersonSelector;

    public GameObject ActivitySelector;

    public Person SelectedPerson;

    public Activity SelectedActivity;

    public GameObject PlayerVariable;

    public GameObject Creator;

    public GameObject GiftButton;

    public TextMeshProUGUI InkAmountText;

    public TextMeshProUGUI PaperAmountText;

    public TextMeshProUGUI BookAmountText;

    public bool inTutorial = false;

    // Start is called before the first frame update
    void Start()
    {
        PlayerVariable = GameObject.Find("Player");

        GeneralSettings setting = new();
        setting.LoadSettings();

        switch(GeneralSettings.Settings.ScreenSize)
        {
            case Resolutions.R1920X1080:
                GetComponent<Transform>().localScale = new Vector3(1, 1, 1);

                break;
            case Resolutions.R3840X2160:
                GetComponent<Transform>().localScale = new Vector3(2, 2, 1);

                break;
            case Resolutions.R1280X1024:
                GetComponent<Transform>().localScale = new Vector3(0.68f, 0.68f, 1);

                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SelectBlackmailLetter()
    {
        if (CurrentLetter != null)
        {
            CurrentLetter.SetActive(false);
        }
        EnterCreator();
    }


    public void SelectGiftLetter()
    {

        if (PlayerVariable.GetComponent<InvScript>().Books > 0 || inTutorial)
        {
            if (CurrentLetter != null)
            {
                CurrentLetter.SetActive(false);
            }
            CurrentLetter = GiftLetter;
            GiftLetter.SetActive(true);
        }
 
    }

    public void SelectRevolutionLetter()
    {
        if(CurrentLetter != null)
        {
            CurrentLetter.SetActive(false);
        }
        CurrentLetter = RevolutionLetter;
        RevolutionLetter.SetActive(true);
    }

    public void OpenPersonSelector()
    {
        if(CurrentNotebook != null)
        {
            CurrentNotebook.SetActive(false);
        }
        CurrentNotebook = PersonSelector;
        PersonSelector.GetComponent<PersonSelectorBehavior>().SetPeople(People);
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
            OpenActivitySelector();
        }
        else if (CurrentLetter.name == "GiftLetter")
        {
            CurrentLetter.GetComponent<GiftLetterBehavior>().SelectPerson(person);
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
            ActivitySelector.GetComponent<ActionSelectorBehavior>().SetActivities(SelectedPerson.SeenActivities
            .Select(x => GameObject.FindObjectsOfType<Activity>().First(y => y.name == x.SystemName && y.Name == x.ReadableName))
            .Where(
                    x => BannedActivitiesObject.GetComponent<BannedActivitiesBehavior>().BannedActivities.Contains(x)
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
                letter.ManipulationLevelIncrease = BannedActivitiesObject.GetComponent<BannedActivitiesBehavior>().GetBanLevel(SelectedActivity);
                letter.Description = "Letter to " + SelectedPerson.Name + " regarding " + SelectedActivity.Name + ".";
                if (SelectedActivity.Name.Equals("programming video games"))
                {
                    letter.Type = LetterType.Gaming;
                }
                else
                {
                    letter.Type = LetterType.Blackmail;
                }
                var player = PlayerVariable.GetComponent<Player>();
                player.invScript.AddLetter(letter);
                player.invScript.Pens--;
                player.invScript.Paper--;
                var seenActivity = player.PeopleKnown[letter.Recieving.Name].SeenActivities.First(x => x.SystemName == SelectedActivity.name && x.ReadableName == SelectedActivity.Name);
                player.PeopleKnown[letter.Recieving.Name].SeenActivities.Remove(seenActivity);
                Debug.Log("Wrote a letter to " + SelectedPerson.Name + " about " + SelectedActivity.Name + " affecting morale by " + letter.ManipulationLevelIncrease);

                // Check on writing letter achievement.
                AchievementItem achItem = AchievementList.GetItem(Achievement.MightierThanTheSword);
                if (!achItem.isDone)
                {
                    AchievementList.MakeAchievement(achItem, player.achievementList);
                }

                LeaveCreator();
            }
        }
        else if(CurrentLetter.name == "RevolutionLetter")
        {
            PlayerVariable.GetComponent<Player>().Revolt();
            LeaveCreator();
        }
        else if(CurrentLetter.name == "GiftLetter")
        {
            Letter letter = new Letter();
            letter.Recieving = SelectedPerson;
            letter.ManipulationLevelIncrease = 2;
            letter.Description = "Letter to " + SelectedPerson.Name + " gifting a Freedom book.";
            letter.Type = LetterType.Gift;
            var player = PlayerVariable.GetComponent<Player>();
            player.invScript.AddLetter(letter);
            player.invScript.Pens--;
            player.invScript.Paper--;
            player.invScript.Books--;
            Debug.Log("Wrote a letter to " + SelectedPerson.Name + " gifting a Freedom book affecting morale by 2.");

            // Check on writing letter achievement.
            AchievementItem achItem = AchievementList.GetItem(Achievement.MightierThanTheSword);
            if (!achItem.isDone)
            {
                AchievementList.MakeAchievement(achItem, player.achievementList);
            }

            LeaveCreator();
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
        PlayerVariable = GameObject.Find("Player");
        CurrentLetter = BlackmailLetter;
        CurrentLetter.SetActive(true);
        BlackmailLetter.GetComponent<BlackmailLetterBehavior>().SelectPerson(null);
        People = PlayerVariable.GetComponent<Player>().PeopleKnown.Values.OrderBy(x => x.Name).ToList();
        InkAmountText.text = $"{PlayerVariable.GetComponent<Player>().invScript.Pens}";
        PaperAmountText.text = $"{PlayerVariable.GetComponent<Player>().invScript.Paper}";
        BookAmountText.text = $"{PlayerVariable.GetComponent<Player>().invScript.Books}";

        if (!inTutorial)
        {
            if (PlayerVariable.GetComponent<Player>().invScript.Books < 1)
            {
                GiftButton.SetActive(false);
            }
            else
            {
                GiftButton.SetActive(true);
            }
        }
        Creator.SetActive(true);
    }
}
