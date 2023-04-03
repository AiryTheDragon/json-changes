using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using UnityEngine;
using TMPro;

public class InventoryMenu : MonoBehaviour, IManualUpdate
{
    public Player player;

    public GameObject LeftPage;

    public GameObject RightPage;

    public int LeftPageLines;

    private int currentPage = 1;
    private int pages = 1;

    private List<string> inventoryList;

    public void TurnPageRight()
    {
        currentPage++;
        ManualUpdate();
    }

    public void TurnPageLeft()
    {
        currentPage--;
        ManualUpdate();
    }

    /*
    public void SetPage()
    {
        if (player is null)
        {
            Start();
            return;
        }

        if (currentPage < 1) currentPage = 1;
        if (currentPage > pages) currentPage = pages;

        CreateInventoryList();

        List<string> readable = inventoryList.Skip((currentPage - 1) * LeftPageLines * 2).Take(LeftPageLines * 2).ToList();

        // list # of paper and pens on top
        StringBuilder leftPageText = new("Paper: ");
        leftPageText.Append(player.invScript.Paper.ToString()).Append("\t\tPens: ");
        leftPageText.Append(player.invScript.Pens);
        StringBuilder rightPageText = new StringBuilder();

        for (int i = 0; i < LeftPageLines && i < readable.Count; i++)
        {
            leftPageText.Append(readable[i]).Append("\n");
        }
        if (readable.Count >= LeftPageLines)
        {
            for (int i = LeftPageLines; i < readable.Count; i++)
            {
                rightPageText.Append(readable[i]).Append("\n");
            }
        }

        LeftPage.GetComponent<TextMeshProUGUI>().text = leftPageText.ToString();
        RightPage.GetComponent<TextMeshProUGUI>().text = rightPageText.ToString();
    }
    */
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        /*
        if (player is null)
        {
            player = Resources.FindObjectsOfTypeAll<Player>().First();
        }
        */
        ManualUpdate();

    }

    public void ManualUpdate()
    {
        if (player is null)
        {
            Start();
            return;
        }



        CreateInventoryList();
        pages = Math.Max(1, (inventoryList.Count - 1)) / (LeftPageLines * 2 - 1) + 1;


        if (currentPage < 1) currentPage = 1;
        if (currentPage > pages) currentPage = pages;

        List<string> readable = inventoryList.Skip((currentPage - 1) * (LeftPageLines * 2 - 1)).Take(LeftPageLines * 2 - 1).ToList();

        // list # of paper and pens on top
        StringBuilder leftPageText = new("Paper: ");
        leftPageText.Append(player.invScript.Paper.ToString()).Append("\tPens: ");
        leftPageText.Append(player.invScript.Pens).Append("\tBooks: ");
        leftPageText.Append(player.invScript.Books).Append("\n");

        StringBuilder rightPageText = new();

        for (int i = 0; i < LeftPageLines - 1 && i < readable.Count; i++)
        {
            leftPageText.Append(readable[i]).Append("\n");
        }
        if (readable.Count >= LeftPageLines - 1)
        {
            for (int i = LeftPageLines - 1; i < readable.Count; i++)
            {
                rightPageText.Append(readable[i]).Append("\n");
            }
        }

        LeftPage.GetComponent<TextMeshProUGUI>().text = leftPageText.ToString();
        RightPage.GetComponent<TextMeshProUGUI>().text = rightPageText.ToString();

        /*
        if(player is null)
        {
            Start();
            return;
        }
        // list # of paper and pens on top
        StringBuilder leftPageText = new("Paper: ");
        leftPageText.Append(player.invScript.Paper.ToString()).Append("\t\tPens: ");
        leftPageText.Append(player.invScript.Pens);

        // list inventory items
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


        */

    }

    private void CreateInventoryList()
    {
        inventoryList = new List<string>();

        for (int i = 0; i < player.invScript.inventoryNames.Count; i++)
        {
            inventoryList.Add(player.invScript.inventoryNames[i]);
        }
        for (int j=0; j<player.invScript.Letters.Count; j++)
        {
            inventoryList.Add(player.invScript.Letters[j].Description);
        }
    }

}
