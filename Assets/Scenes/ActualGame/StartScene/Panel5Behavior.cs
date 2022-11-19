using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace StartScene.MasterPanel
{
    public class Panel5Behavior : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            TextMeshProUGUI elem = GetComponentInChildren<TextMeshProUGUI>();
            var settings = new GeneralSettings();
            settings.LoadSettings();
            switch(GeneralSettings.Settings.ScreenSize)
            {
                case Resolutions.R1920X1080:
                    elem.fontSize = 189;
                    break;
                case Resolutions.R1280X1024:
                    elem.fontSize = 140;
                    break;
            }
        }
    }
}