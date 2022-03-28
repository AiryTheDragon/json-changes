using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GuardBehavior : MonoBehaviour, INeedsClockUpdate
{
    public NPCBehavior MyNPCBehavior;
    
    public GuardConfigurer Configuration;

    public bool Patrolling = true;


    private List<Collider2D> watchedPeople = new List<Collider2D>();

    public GameObject Target;

    // Start is called before the first frame update
    void Start()
    {
        MyNPCBehavior.ActivityTracker.RunActivityGroup(Configuration.PatrolActivityGroup);
        GameObject.Find("Clock").GetComponent<ClockBehavior>().NeedsClockUpdate.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        float rotation = (float)(Math.Atan2(MyNPCBehavior.Velocity.y, MyNPCBehavior.Velocity.x) / (2 * Math.PI));
        GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, rotation * 360 + 90);

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(Patrolling)
        {
            Debug.Log("Collision!");
            if(collider.tag == "Player")
            {
                if(Configuration.EscortOnSight || collider.gameObject.GetComponent<Player>().Suspicion >= collider.gameObject.GetComponent<Player>().MaxSuspicion)
                {
                    MyNPCBehavior.ActivityTracker.RunActivityGroup(Configuration.EscortPlayerActivityGroup);
                    Patrolling = false;
                }
            }
            else if(collider.tag == "NPC")
            {              

                var npc = collider.GetComponent<NPCBehavior>();

                Debug.Log("NPC Collision! with " + npc.Name);


                if (npc.Suspicion >= npc.MaxSuspicion || Configuration.EscortOnSight)
                {
                    Debug.Log("In escorting block");
                    MyNPCBehavior.ActivityTracker.RunActivityGroup(Configuration.EscortNPCActivityGroup);
                    Target = collider.gameObject;
                    Patrolling = false;
                    Debug.Log("Setting Target to " + collider.gameObject.name);
                }
            }
        }
        if(Configuration.SuspicionPerMinute >= 0 && (collider.tag == "Player" || collider.tag == "NPC"))
        {
            watchedPeople.Add(collider);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(watchedPeople.Contains(collider))
        {
            watchedPeople.Remove(collider);
        }
    }

    public void UpdateClock(ClockTime time)
    {
        foreach(var person in watchedPeople)
        {
            if(person.tag == "Player")
            {
                person.gameObject.GetComponent<Player>().Suspicion += Configuration.SuspicionPerMinute;
            }
            else if(person.tag == "NPC")
            {
                person.gameObject.GetComponent<NPCBehavior>().Suspicion += Configuration.SuspicionPerMinute;
            }
        }
    }
}
