using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubbleBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string speechBubbleSize = GeneralSettings.Settings.SpeechBubbleSize;

        Transform transform = GetComponent<Transform>();
        Vector3 scale = transform.localScale;
        Vector3 pos = transform.localPosition;
        if(speechBubbleSize.ToLower() == "large")
        {
            scale.x = 1;
            scale.y = 1;
            pos.x = 3;
            pos.y = 3;
        }
        else if (speechBubbleSize.ToLower() == "medium")
        {
            scale.x = 0.5f;
            scale.y = 0.5f;
            pos.x = 1.5f;
            pos.y = 2;
        }
        else if (speechBubbleSize.ToLower() == "small")
        {
            scale.x = 0.25f;
            scale.y = 0.25f;
            pos.x = 1;
            pos.y = 1.5f;
        }

        transform.localScale = scale;
        transform.localPosition = pos;
    }
}
