using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class PersonDisplayBehavior : MonoBehaviour
{
    public Person DisplayedPerson;

    public GameObject TextComponent;

    public GameObject Notebook;

    void Start()
    {
        TextComponent.GetComponent<TextMeshProUGUI>().text = "";
    }

    public void SelectPerson()
    {
        var script = Notebook.GetComponent<PersonSelectorBehavior>();
        script.SelectPerson(DisplayedPerson);
    }

    public void SetPerson(Person person)
    {
        this.DisplayedPerson = person;
        var tmp = TextComponent.GetComponent<TextMeshProUGUI>();
        if(person is null)
        {
            tmp.text = "";
        }
        else
        {
            string confidenceLevel;
            if(person.ManipulationLevel <= 0)
            {
                confidenceLevel = "Unknown";
            }
            else if (person.ManipulationLevel <= 1)
            {
                confidenceLevel = "Wavering";
            }
            else if (person.ManipulationLevel <= 2)
            {
                confidenceLevel = "Marginal";
            }
            else if (person.ManipulationLevel <= 3)
            {
                confidenceLevel = "OK";
            }
            else if (person.ManipulationLevel <=4)
            {
                confidenceLevel = "High";
            }
            else
            {
                confidenceLevel = "Rock Solid";
            }
            string importanceLevel;
            if(person.Value <= 0)
            {
                importanceLevel = "None";
            }
            else if (person.Value <=1)
            {
                importanceLevel = "Very Low";
            }
            else if (person.Value <=2)
            {
                importanceLevel = "Low";
            }
            else if (person.Value <= 3)
            {
                importanceLevel = "Medium";
            }
            else if (person.Value <=4)
            {
                importanceLevel = "Somewhat High";
            }
            else
            {
                importanceLevel = "Very High";
            }
            tmp.text = $"{person.Name}\nConfidence Level:   {confidenceLevel}\nImportance:    {importanceLevel}";
        }
    }
}
