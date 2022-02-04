using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public GameObject Target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var position = Target.GetComponent<Transform>().position;

        GetComponent<Transform>().position = new Vector3(position.x, position.y, -10);
    }
}
