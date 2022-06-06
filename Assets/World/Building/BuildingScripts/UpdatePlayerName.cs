using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;



public class UpdatePlayerName : MonoBehaviour
{
    public TextMeshPro TextObject;

    private void Start()
    {
        TextObject.text = Player.Name;
    }

}
