using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WritingDeskBehavior : MonoBehaviour
{
    public GameObject LetterCreator;

    // Start is called before the first frame update
    void Start()
    {
        LetterCreator = GameObject.Find("LetterWorkspace");
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
