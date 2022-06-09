using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class SignBehavior : MonoBehaviour
{
    private static CanvasSignBehavior _canvasSign;

    private static CanvasSignBehavior canvasSign
    {
        get { return _canvasSign; }
        set
        {
            if (canvasSign != null) return;
            _canvasSign = value;
        }
    }

    public GameObject thisSign;
    public GameObject thisText;
    public string expandedText="";
    // Start is called before the first frame update
    void Start()
    {
        canvasSign = Resources.FindObjectsOfTypeAll<CanvasSignBehavior>().First();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseUpAsButton()
    {
        if (canvasSign==null)
        {
            Debug.Log("Canvas sign is null");
        }
        else
        {
            Debug.Log("Canvas sign is not null");
        }

        canvasSign.Open(expandedText);
    }

}
