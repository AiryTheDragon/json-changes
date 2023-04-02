using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVStationBehavior : MonoBehaviour, INeedsClockUpdate
{
    public List<GameObject> Monitors;

    public List<GameObject> Cameras;

    public int GuardsWatching = 0;


    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Clock").GetComponent<ClockBehavior>().NeedsClockUpdate.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateClock(ClockTime time)
    {

        if (GuardsWatching > 0)
        {
            Debug.Log("A guard is watching the security cameras.");
            foreach(var camera in Cameras)
            {
                var cameraBehavior = camera.GetComponent<SecurityCameraBehavior>();
                if(cameraBehavior.seesPlayer)
                {
                    cameraBehavior.playerCollision.GetComponentInParent<Player>().AddSuspicion(cameraBehavior.SuspicionPerMinute);
                    Debug.Log("Your behaviors is regarded as suspicious..." + cameraBehavior.SuspicionPerMinute);
                }
            }
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("A " + collision.tag + " is by the security monitors.");


        if (collision.CompareTag("GuardNPC"))
        {
            GuardsWatching++;
            Debug.Log("A guard is watching the security monitors.");
        }
        UpdateMonitors();
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("A " + collision.tag + " left the security monitors.");


        if (collision.CompareTag("GuardNPC"))
        {
            Debug.Log("A guard is no longer watching the security monitors.");
            GuardsWatching--;
            if (GuardsWatching < 0)
            {
                GuardsWatching = 0;
            }
        }
        UpdateMonitors();
    }
    /*
    public void OnCollisionEnter2D(Collision2D collision)
    {
        
        Debug.Log("A " + collision.collider.tag + " is by the security monitors.");


        if(collision.collider.tag == "GuardNPC")
        {
            GuardsWatching++;
            Debug.Log("A guard is watching the security monitors.");
        }
        UpdateMonitors();
        
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        
        if(collision.collider.tag == "GuardNPC")
        {
            Debug.Log("A guard is no longer watching the security monitors.");
            GuardsWatching--;
            if(GuardsWatching < 0)
            {
                GuardsWatching = 0;
            }
        }
        UpdateMonitors();
        
    }
    */
    public void UpdateMonitors()
    {
        if(GuardsWatching > 0)
        {
            foreach(var monitor in Monitors)
            {
                monitor.GetComponent<SpriteRenderer>().color = new Color(0, 128, 0, 255);
            }
        }
        else
        {
            foreach(var monitor in Monitors)
            {
                monitor.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 255);
            }
        }
    }
}
