using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVision : MonoBehaviour
{
    public GameObject parentScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            parentScript.GetComponent<SecurityCameraBehavior>().seesPlayer = true;
            parentScript.GetComponent<SecurityCameraBehavior>().playerCollision = collider;
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            parentScript.GetComponent<SecurityCameraBehavior>().seesPlayer = false;
        }
    }
}
