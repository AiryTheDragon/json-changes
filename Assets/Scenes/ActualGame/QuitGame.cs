using System.Collections;
using System.Collections.Generic;
using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{
    public void BtnQuit()
    {
        #if !(UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX || STEAMWORKS_WIN || STEAMWORKS_LIN_OSX)
            #define DISABLESTEAMWORKS
        #endif

        #if !DISABLESTEAMWORKS
            SteamAPI.Shutdown();
        #endif
        Application.Quit();
    }


}
