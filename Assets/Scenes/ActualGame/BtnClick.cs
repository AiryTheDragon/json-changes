using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnClick : MonoBehaviour
{
    public void BtnNewScene()
    {

        try
        {
            string saveLocation = System.IO.Path.Combine(Application.persistentDataPath, "EEData/saves");
            Directory.CreateDirectory(saveLocation);
            string filePath = Path.Combine(saveLocation, $"save{Player.SaveFile}.json");
            using var fileStream = File.Create(filePath);
            using StreamWriter fileWriter = new(fileStream);
            fileWriter.Write($"{{\"PlayerName\":\"{Player.Name}\",\"Time\":{{\"Hour\":6,\"Minute\":50,\"Day\":0}}}}");
        }
        catch (Exception)
        {

        }
        SceneManager.LoadScene("MainScene");
    }


}
