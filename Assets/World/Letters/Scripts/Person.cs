using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person
{
    public List<Activity> SeenActivities = new List<Activity>();

    public string Name;

    public string PositionName;

    public int Value;

    public int ManipulationLevel;

    public string GetManipulationLevelText()
    {
        string confidenceLevel;
        if(ManipulationLevel <= 0)
        {
            confidenceLevel = "Unknown";
        }
        else if (ManipulationLevel <= 1)
        {
            confidenceLevel = "Wavering";
        }
        else if (ManipulationLevel <= 2)
        {
            confidenceLevel = "Marginal";
        }
        else if (ManipulationLevel <= 3)
        {
            confidenceLevel = "OK";
        }
        else if (ManipulationLevel <=4)
        {
            confidenceLevel = "High";
        }
        else
        {
            confidenceLevel = "Rock Solid";
        }
        return confidenceLevel;
    }

    public string GetValueText()
    {
        string importanceLevel;
        if(Value <= 0)
        {
            importanceLevel = "None";
        }
        else if (Value <=1)
        {
            importanceLevel = "Very Low";
        }
        else if (Value <=2)
        {
            importanceLevel = "Low";
        }
        else if (Value <= 3)
        {
            importanceLevel = "Medium";
        }
        else if (Value <=4)
        {
            importanceLevel = "Somewhat High";
        }
        else
        {
            importanceLevel = "Very High";
        }
        return importanceLevel;
    }
}
