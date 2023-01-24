using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WritingDeskBehavior : MonoBehaviour
{
    public LetterCreator letterCreator;

    // Start is called before the first frame update
    void Start()
    {
        //LetterCreator = Resources.FindObjectsOfTypeAll<LetterCreator>().First().gameObject;
        letterCreator = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().letterCreator;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenLetterCreator()
    {
       

        letterCreator.EnterCreator();
    }
}
