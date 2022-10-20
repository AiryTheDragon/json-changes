using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleFullscreenBehavior : MonoBehaviour
{
    private bool buttonDownLast = false;
    private static FullScreenMode fsMode = FullScreenMode.ExclusiveFullScreen;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.F11))
        {
            if(!buttonDownLast)
            {
                bool fscreen = !Screen.fullScreen;
                Screen.fullScreen = fscreen;
                if(fscreen)
                {
                    Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                }
                else
                {
                    Screen.fullScreenMode = FullScreenMode.Windowed;
                }
                fsMode = Screen.fullScreenMode;
                buttonDownLast = true;
            }
            
        }
        else
        {
            buttonDownLast = false;
        }
    }
}
