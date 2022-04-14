using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvScript : MonoBehaviour
{

    public List<GameObject> inventoryObjects = new List<GameObject>();
    public List<string> inventoryNames = new List<string>();
    public List<Letter> Letters = new List<Letter>();

    public int Paper = 0;

    public int Pens = 0;

    private Dictionary<string, GameObject> inventoryDict = new Dictionary<string, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addItem(GameObject gameObject)
    {
        if (gameObject.tag=="Key")
        {
            string thisName = gameObject.GetComponent<KeyScript>().objectName;

            inventoryNames.Add(thisName);
            inventoryObjects.Add(gameObject);
            inventoryDict.Add(thisName, gameObject);

        }
        else if(gameObject.tag == "Paper")
        {
            Debug.Log("Added Paper");
            Paper++;
        }
        else if(gameObject.tag == "Pen")
        {
            Debug.Log("Added pen");
            Pens++;
        }
        else if (gameObject.tag == "Cupcake")
        {
            string thisName = gameObject.GetComponent<KeyScript>().objectName;
            inventoryNames.Add(thisName);
            inventoryObjects.Add(gameObject);
            Debug.Log(thisName + "   " + gameObject.name);
            inventoryDict.Add(thisName, gameObject);
        }
    }

    public bool haveItem(string objectName)
    {
        if(inventoryDict.ContainsKey(objectName))
        {
            return true;
        }
        return false;
    }

    public void AddLetter(Letter letter)
    {
        Letters.Add(letter);
    }

    public void RemoveLetter(Letter letter)
    {
        Letters.Remove(letter);
    }


    public void removeItem(string name)
    {
        //TODO
    }

    public void removeItem(GameObject gameObject)
    {
        //TODO
    }



}
