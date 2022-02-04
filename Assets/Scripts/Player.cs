using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float slowSpeed = 1f;
    [SerializeField] float walkSpeed = 2f;
    [SerializeField] float runSpeed = 4f;
    [SerializeField] float currentSpeed = 2f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
 ;
        if (Input.GetAxis("Fire1")>0)
        {
            currentSpeed = runSpeed;
        }
        else if (Input.GetAxis("Fire2") > 0)
        {
            currentSpeed = slowSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }

        float xChange = Input.GetAxis("Horizontal") * currentSpeed * Time.deltaTime;
        float yChange = Input.GetAxis("Vertical") * currentSpeed * Time.deltaTime;

        transform.Translate(xChange, 0, 0);
        transform.Translate(0, yChange, 0);
    }

}
