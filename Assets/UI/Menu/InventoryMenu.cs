using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TMPro;

public class InventoryMenu : MonoBehaviour, IManualUpdate
{
    Player player;

    public GameObject LeftPage;

    // Start is called before the first frame update
    void Start()
    {
        player = Resources.FindObjectsOfTypeAll<Player>().First();
        ManualUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ManualUpdate()
    {
        if(player is null)
        {
            Start();
        }
        StringBuilder text = new("Paper: ");
        text.Append(player.invScript.Paper.ToString()).Append("\t\tPens: ");
        text.Append(player.invScript.Pens);
        int i = 0;
        for(i = 0; i < player.invScript.inventoryNames.Count && i < 13; i++)
        {
            text.Append("\n").Append(player.invScript.inventoryNames[i]);
        }

        for(; i < player.invScript.inventoryNames.Count && i < 27; i++)
        {
            text.Append("\n").Append(player.invScript.inventoryNames[i]);
        }
        LeftPage.GetComponent<TextMeshProUGUI>().text = text.ToString();
    }
}
