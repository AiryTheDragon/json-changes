using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnClickToNameScene : MonoBehaviour
{
    public void BtnNewScene()
    {
        SceneManager.LoadScene("NameScene");
    }


}
