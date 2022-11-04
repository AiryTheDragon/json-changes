using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugButtonScript : MonoBehaviour
{
    public void OpenBugReport()
    {
        Application.OpenURL("https://forms.gle/dKcLpQyrFDEPx6VSA");
    }
}
