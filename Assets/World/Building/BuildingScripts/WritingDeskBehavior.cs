using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WritingDeskBehavior : MonoBehaviour
{
    public GameObject LetterCreator;

    // Start is called before the first frame update
    void Start()
    {
        LetterCreator = Resources.FindObjectsOfTypeAll<LetterCreator>().First().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenLetterCreator()
    {
        LetterCreator.GetComponent<LetterCreator>().EnterCreator();
    }
}
