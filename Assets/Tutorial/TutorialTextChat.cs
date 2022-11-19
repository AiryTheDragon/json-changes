using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialTextChat : MonoBehaviour
{

    public GameObject TextObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setText(string text)
    {
        TextObject.GetComponent<TextMeshProUGUI>().text = text;
    }
}
