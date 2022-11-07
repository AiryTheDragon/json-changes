using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReturnToPanel : MonoBehaviour
{ 
    public GameObject NextPanel;

    public AudioSource audioSource;

    public void Submit()
    {
        audioSource.Play();
        gameObject.SetActive(false);
        NextPanel.SetActive(true);
    }

}