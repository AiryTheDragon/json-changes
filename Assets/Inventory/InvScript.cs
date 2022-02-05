using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvScript : MonoBehaviour
{

    public List<GameObject> inventoryObjects = new List<GameObject>();
    public List<string> inventoryNames = new List<string>();
    public List<Letter> Letters = new List<Letter>();

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

            inventoryNames.Add(gameObject.GetComponent<KeyScript>().objectName);
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
