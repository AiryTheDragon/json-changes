using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCInfoBehavior : MonoBehaviour
{
    public TextMeshProUGUI TextComponent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenNPCInfo(NPCBehavior behavior)
    {
        Person person = behavior.GetPersonInformation();
        if(person is null)
        {
            TextComponent.text = "";
        }
        else
        {
            string activityName = "";
            if(behavior.ActivityTracker?.RunningActivity != null)
            {
                activityName = behavior.ActivityTracker.RunningActivity.Name;
            }
            TextComponent.text = $"Name:  {person.Name}\nPosition:   {person.PositionName}\n"
            + $"Value:   {person.GetValueText()}\nConfidence:   {person.GetManipulationLevelText()}\n" +
            $"Currently:   {activityName}";
        }
        this.gameObject.SetActive(true);
    }

    public void OpenMessage(string text)
    {
        TextComponent.text = text;
        this.gameObject.SetActive(true);
    }

    public void AchievementInfo(AchievementItem item)
    {

        if (item is null)
        {
            TextComponent.text = "";
        }
        else
        {
            TextComponent.text = $"New Achievement!\n\n"
            + $"Achievement:  {item.AchievementName}\nDescription:   {item.AchievementDescription}\n";
        }
        this.gameObject.SetActive(true);
    }


    public void OnClick()
    {
        this.gameObject.SetActive(false);
    }
}
