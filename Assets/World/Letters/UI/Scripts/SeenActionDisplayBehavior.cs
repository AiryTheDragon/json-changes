using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SeenActionDisplayBehavior : MonoBehaviour
{
    public Activity DisplayActivity;

    public GameObject TextObject;

    public GameObject ActivityNotebook;

    // Start is called before the first frame update
    void Start()
    {
        SetDisplayActivity(DisplayActivity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectActivity()
    {
        if(DisplayActivity != null)
        {
            ActivityNotebook.GetComponent<ActionSelectorBehavior>().SelectActivity(DisplayActivity);
        }
    }

    public void SetDisplayActivity(Activity activity)
    {
        DisplayActivity = activity;
        if(activity != null)
        {
            TextObject.GetComponent<TextMeshProUGUI>().text = activity.Name;
        }
        else
        {
            TextObject.GetComponent<TextMeshProUGUI>().text = "";
        }
    }
}
