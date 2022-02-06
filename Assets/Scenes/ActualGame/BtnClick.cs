using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnClick : MonoBehaviour
{
    public void BtnNewScene()
    {

        Player.Name = "George";
        //PlayerPrefs.SetInt("score", 2500);
        SceneManager.LoadScene("MainScene");
    }


}
