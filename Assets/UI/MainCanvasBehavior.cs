using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvasBehavior : MonoBehaviour
{
    public GameObject Menu;

    public GameObject LetterWorkspace;

    /// <summary>
    /// Returns true if any menu that could cover a clickable is open.
    /// </summary>
    public bool AnyMenuOpen()
    {
        return Menu.activeInHierarchy || LetterWorkspace.activeInHierarchy;
    }
}
