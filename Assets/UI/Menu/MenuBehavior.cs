using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBehavior : MonoBehaviour, IManualUpdate
{
    TabBehavior selectedTab;

    public GameObject inventoryTab;
    public GameObject escapeTab;
    public GameObject parent;

    private bool started;

    private Player _player;
    private Player player { get {return _player;} set {
        if(player != null) return;
        _player = value;
    }}

    public bool escDown;
    public bool armed;

    // Start is called before the first frame update
    void Start()
    {
        if(!started)
        {
            started = true;
            selectedTab = null;
            //SelectTab(inventoryTab.GetComponent<TabBehavior>());
            player = Resources.FindObjectsOfTypeAll<Player>().First();
        }


        var settings = new GeneralSettings();
        settings.LoadSettings();

        switch(GeneralSettings.Settings.ScreenSize)
        {
            case Resolutions.R1920X1080:
                GetComponent<Transform>().position = new Vector3(0, 0, 0);
                GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
                break;
            case Resolutions.R1280X1024:
                GetComponent<Transform>().position = new Vector3(0, 0, 0);
                GetComponent<Transform>().localScale = new Vector3(0.9f, 0.9f, 1);
                break;
            case Resolutions.R3840X2160:
                GetComponent<Transform>().position = new Vector3(0, 0, 0);
                GetComponent<Transform>().localScale = new Vector3(2.1f, 2.1f, 1);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            if(!escDown)
            {
                escDown = true;
            }
        }
        else if(escDown)
        {
            escDown = false;
            if(!armed)
            {
                armed = true;
            }
            else
            {
                Close();
            }
        }
    }
    
    public void SelectTab(TabBehavior tab)
    {
        tab.Select();
        if(selectedTab != tab)
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
        if(!started)
        {
            Start();
        }
        player.inputEnabled = false;
        parent.SetActive(true);
        selectedTab = null;
        if(key == KeyCode.Escape)
        {
            SelectTab(escapeTab.GetComponent<TabBehavior>());
            escDown = true;
            armed = false;
        }
        else if(key == KeyCode.I)
        {
            SelectTab(inventoryTab.GetComponent<TabBehavior>());
        }
        ManualUpdate();
    }

    public void Close()
    {
        selectedTab?.Deselect();
        selectedTab = null;
        player.inputEnabled = true;
        parent.SetActive(false);
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
