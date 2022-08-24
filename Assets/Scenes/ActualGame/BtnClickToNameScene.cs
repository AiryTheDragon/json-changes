using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnClickToNameScene : MonoBehaviour
{
    public GameObject NextPanel;
    public GameObject ParentPanel;

    public void BtnNewScene()
    {
        //SceneManager.LoadScene("NameScene");
        // Meh I'll hack this in.

        NextPanel.SetActive(true);
        ParentPanel.SetActive(false);
    }


}
