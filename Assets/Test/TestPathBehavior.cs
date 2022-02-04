using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPathBehavior : MonoBehaviour
{
    public GameObject Target;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(Target.GetComponent<Transform>().position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
