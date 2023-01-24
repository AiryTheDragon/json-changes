using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GiftLetterBehavior : MonoBehaviour
{
    public GameObject PersonText;

    public Person SelectedPerson;


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
        if (person != null)
        {
            PersonText.GetComponent<TextMeshProUGUI>().text = person.Name;
        }
        else
        {
            PersonText.GetComponent<TextMeshProUGUI>().text = "(Select Name)";
        }
    }

}
