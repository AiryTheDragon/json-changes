using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    [SerializeField] string doorKeyName="HomeKey";
    [SerializeField] bool needKey = true;

    [SerializeField] private AudioClip _doorClose = null;
    [SerializeField] private AudioClip _doorOpen = null;

    private AudioSource _source = null;

    public GameObject openDoorObject;
    public GameObject closedDoorObject;

    public InvScript playerInv;

    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();
        if (_source == null)
        {
            Debug.Log("Audio Source is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!needKey || playerInv.haveItem(doorKeyName))
        {
            closedDoorObject.GetComponent<SpriteRenderer>().enabled = false;
            closedDoorObject.GetComponent<BoxCollider2D>().enabled = false;
            openDoorObject.GetComponent<SpriteRenderer>().enabled = true;
            openDoorObject.GetComponent<BoxCollider2D>().enabled = true;

            _source.clip = _doorOpen;
            _source.Play();


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        closedDoorObject.GetComponent<SpriteRenderer>().enabled = true;
        closedDoorObject.GetComponent<BoxCollider2D>().enabled = true;
        openDoorObject.GetComponent<SpriteRenderer>().enabled = false;
        openDoorObject.GetComponent<BoxCollider2D>().enabled = false;

        if (!needKey || playerInv.haveItem(doorKeyName))
        {
            _source.clip = _doorClose;
            _source.Play();
        }
    }


    void openDoor()
    {

    }

    void closeDoor()
    {

    }


}
