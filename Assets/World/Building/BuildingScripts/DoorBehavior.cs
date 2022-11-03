using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    public int colliding = 0;
    bool open = false;

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
        if (collision.tag == "Player" && (!needKey || playerInv.haveItem(doorKeyName)))
        {
            if(!open)
            {
                OpenDoor(true);
            }
            colliding++;
        }
        else if(collision.tag == "GuardNPC" || collision.tag == "NPC")
        {
            if(!open)
            {
                OpenDoor(true);
            }
            colliding++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "GuardNPC" || collision.tag == "NPC")
        {
            colliding = Math.Max(0, colliding - 1);
        }
        else if (collision.tag == "Player" && (!needKey || playerInv.haveItem(doorKeyName)))
        {
            colliding = Math.Max(0, colliding - 1);
        }

        if (colliding == 0 && open)
        {
            CloseDoor(collision.tag == "Player");
        }
        
    }


    public void OpenDoor(bool playSound)
    {
        closedDoorObject.GetComponent<SpriteRenderer>().enabled = false;
        closedDoorObject.GetComponent<BoxCollider2D>().enabled = false;

        var shadowCaster = closedDoorObject.GetComponent<UnityEngine.Rendering.Universal.ShadowCaster2D>();

        //closedDoorObject.GetComponent<UnityEngine.Rendering.Universal.ShadowCaster2D>().enabled = false;
        closedDoorObject.GetComponent<UnityEngine.Rendering.Universal.ShadowCaster2D>().castsShadows = false;
        openDoorObject.GetComponent<SpriteRenderer>().enabled = true;
        openDoorObject.GetComponent<BoxCollider2D>().enabled = true;
        //openDoorObject.GetComponent<UnityEngine.Rendering.Universal.ShadowCaster2D>().enabled = false;
        openDoorObject.GetComponent<UnityEngine.Rendering.Universal.ShadowCaster2D>().castsShadows = true;

        if(playSound)
        {
            _source.clip = _doorOpen;
            _source.Play();
        }
        open = true;
    }

    public void CloseDoor(bool playSound)
    {
        closedDoorObject.GetComponent<SpriteRenderer>().enabled = true;
        closedDoorObject.GetComponent<BoxCollider2D>().enabled = true;
        closedDoorObject.GetComponent<UnityEngine.Rendering.Universal.ShadowCaster2D>().castsShadows = true;
        openDoorObject.GetComponent<SpriteRenderer>().enabled = false;
        openDoorObject.GetComponent<BoxCollider2D>().enabled = false;
        openDoorObject.GetComponent<UnityEngine.Rendering.Universal.ShadowCaster2D>().castsShadows = false;


        if (playSound)
        {
            _source.clip = _doorClose;
            _source.Play();
        }
        open = false;
    }


}
