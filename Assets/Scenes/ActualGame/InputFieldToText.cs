using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputFieldToText : MonoBehaviour
{
    public TextMeshProUGUI actualText;
    public TextMeshProUGUI letterText;

    public GameObject NextPanel;

    public AudioSource audioSource;

    public void Submit()
    {
        Player.Name = actualText.text;
        if(string.IsNullOrWhiteSpace(Player.Name))
        {
            Player.Name = "Jeff";
        }
        letterText.SetText($"Dear {Player.Name},\n\nIn concordance with Senate Law 902.3b, a new mandate has been established.\n\nAll writing tools, " +
            "including but not limited to pens and paper are hereby declaired illegal.\n\n" +
            "If you find this troublesome, you are, as in all things, welcome to submit yourself for reconditioning.\n\n" +
            "Hard times bring peace,\nYour Local Area Manager");
        audioSource.Play();
        gameObject.SetActive(false);
        NextPanel.SetActive(true);
    }

}