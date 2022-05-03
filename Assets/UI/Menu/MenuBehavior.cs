using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBehavior : MonoBehaviour, IManualUpdate
{
    TabBehavior selectedTab;

    public GameObject inventoryTab;
    public GameObject escapeTab;
    public GameObject parent;


    // Start is called before the first frame update
    void Start()
    {
        SelectTab(inventoryTab.GetComponent<TabBehavior>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SelectTab(TabBehavior tab)
    {
        tab.Select();
        selectedTab?.Deselect();
        selectedTab = tab;
    }

    public void ManualUpdate()
    {
        if(selectedTab != null && selectedTab.menu != null)
        {
            IManualUpdate x = selectedTab.menu.GetComponentInChildren<IManualUpdate>();
            if(x != null)
            {
                x.ManualUpdate();
            }
        }
    }

    public void Open(KeyCode key)
    {
        parent.SetActive(true);
        if(key == KeyCode.Escape)
        {
            SelectTab(escapeTab.GetComponent<TabBehavior>());
        }
        else if(key == KeyCode.I)
        {
            SelectTab(inventoryTab.GetComponent<TabBehavior>());
        }
        ManualUpdate();
    }

    public bool Active()
    {
        if(parent != null)
        {
            return parent.activeInHierarchy;
        }
        return false;
    }
}
