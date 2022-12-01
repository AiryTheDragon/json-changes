using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvasBehavior : MonoBehaviour
{
    public GameObject Menu;

    public GameObject LetterWorkspace;

    public GameObject ConfirmationMenu;

    public GameObject Minimap;

    public GameObject SuspicionBar;

    public GameObject BugReportButton;

    public GameObject ClockTimeDisplay;

    public GameObject NPCInfoMenu;


    public void Start()
    {
        var settings = new GeneralSettings();
        settings.LoadSettings();

        switch(GeneralSettings.Settings.ScreenSize)
        {
            case Resolutions.R1920X1080:
                // Setup Always Visible elements.
                Minimap.GetComponent<Transform>().localPosition = new Vector3(-721, 344, 0);
                Minimap.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
                
                SuspicionBar.GetComponent<RectTransform>().anchoredPosition = new Vector3(817, 452, 0);
                SuspicionBar.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

                BugReportButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(815, 376, 0);
                BugReportButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

                ClockTimeDisplay.GetComponent<RectTransform>().anchoredPosition = new Vector3(-865, 466, 0);
                ClockTimeDisplay.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 100);
                ClockTimeDisplay.GetComponent<Text>().fontSize = 42;

                NPCInfoMenu.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                NPCInfoMenu.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

                ConfirmationMenu.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                break;
            case Resolutions.R3840X2160:
                // Setup Always Visible elements.
                Minimap.GetComponent<Transform>().localPosition = new Vector3(-1446, 645, 0);
                Minimap.GetComponent<Transform>().localScale = new Vector3(2, 2, 1);
                
                SuspicionBar.GetComponent<RectTransform>().anchoredPosition = new Vector3(1634, 904, 0);
                SuspicionBar.GetComponent<RectTransform>().localScale = new Vector3(2, 2, 1);

                BugReportButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(1630, 752, 0);
                BugReportButton.GetComponent<RectTransform>().localScale = new Vector3(2, 2, 1);

                ClockTimeDisplay.GetComponent<RectTransform>().anchoredPosition = new Vector3(-1752, 894, 0);
                ClockTimeDisplay.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 200);
                ClockTimeDisplay.GetComponent<Text>().fontSize = 84;

                NPCInfoMenu.GetComponent<RectTransform>().localScale = new Vector3(2, 2, 1);
                NPCInfoMenu.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

                ConfirmationMenu.GetComponent<RectTransform>().localScale = new Vector3(2, 2, 1);
                break;
        }
    }

    /// <summary>
    /// Returns true if any menu that could cover a clickable is open.
    /// </summary>
    public bool AnyMenuOpen()
    {
        return Menu.activeInHierarchy || LetterWorkspace.activeInHierarchy || ConfirmationMenu.activeInHierarchy;
    }
}
