using System.Diagnostics;
using System;
using System.IO;
using System.Linq.Expressions;
using Newtonsoft.Json;
using UnityEngine;

public class GeneralSettings
{
    public static Settings Settings { get; private set; }
    public void LoadSettings()
    {
        if(Settings is not null) return;
        try{
            string fileFolder = Application.persistentDataPath;
            string pathString = System.IO.Path.Combine(fileFolder, "EEData/Settings.json");

            using FileStream stream = File.OpenRead(pathString);
            using StreamReader reader = new (stream);
            string fileData = reader.ReadToEnd();

            Settings = JsonConvert.DeserializeObject<Settings>(fileData);

        } catch (Exception)
        {
            Settings = new Settings();
            SaveSettings();
        }
    }

    public void SaveSettings()
    {
        if (Settings is null) return;
        try {
            string fileData = JsonConvert.SerializeObject(Settings);
            string pathString = System.IO.Path.Combine(Application.persistentDataPath, "EEData/Settings.json");

            using FileStream stream = File.Open(pathString, FileMode.Truncate, FileAccess.Write);
            using StreamWriter writer = new(stream);

            writer.Write(fileData);
            writer.Flush();

        } catch (Exception)
        {

        }
    }
}