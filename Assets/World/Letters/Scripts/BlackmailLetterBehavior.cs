using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlackmailLetterBehavior : MonoBehaviour
{
    public GameObject PersonText;

    public GameObject ActivityText;

    public Person SelectedPerson;

    public Activity SelectedActivity;


    // Start is called before the first frame update
    void Start()
    {
        SelectPerson(null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectPerson(Person person)
    {
        if(person != null)
        {
            PersonText.GetComponent<TextMeshProUGUI>().text = person.Name;
            if(SelectedActivity != null && !person.SeenActivities.Where(x => x.Name == SelectedActivity.Name).Any())
            {
                SelectActivity(null);
            }
        }
        else
        {
            PersonText.GetComponent<TextMeshProUGUI>().text = "(Select Name)";
            SelectActivity(null);
        }
    }

    public void SelectActivity(Activity activity)
    {
        if(activity != null)
        {
            ActivityText.GetComponent<TextMeshProUGUI>().text = activity.Name;
        }
        else
        {
            ActivityText.GetComponent<TextMeshProUGUI>().text = "(Select Activity)";
        }
    }
}
