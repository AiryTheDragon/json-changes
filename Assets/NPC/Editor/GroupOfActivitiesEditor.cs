using UnityEditor;
using UnityEngine;
using System.Collections;
using System;


/*[CustomEditor(typeof(GroupOfActivities)), CanEditMultipleObjects]
public class GroupOfActivitiesEditor : Editor
{
    int hour = 8;
    int minute = 0;

    GroupOfActivities myGroup;

    public override void OnInspectorGUI()
    {
        myGroup = target as GroupOfActivities;
        if(myGroup.startTime == null)
        {
            myGroup.startTime = new ClockTime();
            myGroup.startTime.SetTime(0, Math.Clamp(hour, 0, 23), Math.Clamp(minute, 0, 59));

        }
        DrawDefaultInspector();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Start Time:");
        EditorGUI.BeginChangeCheck();

        hour = EditorGUILayout.IntField("Hour: ", hour);
        minute = EditorGUILayout.IntField("Minute: ", minute);

        if(EditorGUI.EndChangeCheck())
        {
            if(myGroup != null)
            {
                
                hour = Math.Clamp(hour, 0, 23);
                minute = Math.Clamp(minute, 0, 59);
                myGroup.startTime.SetTime(0, Math.Clamp(hour, 0, 23), Math.Clamp(minute, 0, 59));

            }
        }
    }
}*/