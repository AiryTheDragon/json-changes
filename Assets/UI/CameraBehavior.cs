using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public GameObject Target;


    // Update is called once per frame
    void LateUpdate()
    {
        var position = Target.GetComponent<Transform>().position;

        GetComponent<Transform>().position = new Vector3(position.x, position.y, -10);
    }
}
