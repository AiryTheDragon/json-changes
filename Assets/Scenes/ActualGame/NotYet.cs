using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotYet : MonoBehaviour
{
    public TextMeshProUGUI letterText;

    public GameObject NextPanel;

    public AudioSource audioSource;

    public void Submit()
    {
        letterText.SetText($"Dear Player,\n\nThis feature is not implemented yet.  Please choose another option." +
            "\n\nExtra options bring conflict,\nYour Local Area Manager");
        audioSource.Play();
        gameObject.SetActive(false);
        NextPanel.SetActive(true);
    }

}
