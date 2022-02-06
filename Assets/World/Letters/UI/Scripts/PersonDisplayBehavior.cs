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
        SetPerson(DisplayedPerson);
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
            tmp.SetText("");
        }
        else
        {
            string confidenceLevel = person.GetManipulationLevelText();

            string importanceLevel = person.GetValueText();
            tmp.SetText($"{person.Name}\nConfidence Level:   {confidenceLevel}\nImportance:    {importanceLevel}");
        }
    }
}
