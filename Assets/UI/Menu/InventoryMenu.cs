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

    public GameObject RightPage;

    public int LeftPageLines;

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
            return;
        }
        StringBuilder leftPageText = new("Paper: ");
        leftPageText.Append(player.invScript.Paper.ToString()).Append("\t\tPens: ");
        leftPageText.Append(player.invScript.Pens);
        int i = 0;
        for(i = 0; i < player.invScript.inventoryNames.Count && i < LeftPageLines; i++)
        {
            leftPageText.Append("\n").Append(player.invScript.inventoryNames[i]);
        }

        StringBuilder rightPageText = new();

        for(; i < player.invScript.inventoryNames.Count && i < 27; i++)
        {
            rightPageText.Append("\n").Append(player.invScript.inventoryNames[i]);
        }
        LeftPage.GetComponent<TextMeshProUGUI>().text = leftPageText.ToString();
        RightPage.GetComponent<TextMeshProUGUI>().text = rightPageText.ToString();
    }
}
