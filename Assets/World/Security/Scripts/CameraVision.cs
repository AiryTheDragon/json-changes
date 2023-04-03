using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVision : MonoBehaviour
{
    public GameObject parentScript;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            parentScript.GetComponent<SecurityCameraBehavior>().seesPlayer = true;
            parentScript.GetComponent<SecurityCameraBehavior>().playerCollision = collider;
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            parentScript.GetComponent<SecurityCameraBehavior>().seesPlayer = false;
        }
    }
}
