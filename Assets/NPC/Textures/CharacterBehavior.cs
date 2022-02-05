using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehavior : MonoBehaviour
{

    public float HeadRange;

    public float BumpLevel;

    public Transform HeadPosition;

    public Vector3 OriginalHeadPosition;

    public float MaxVelocity;

    public GameObject Head;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHead(float xSpeed, float ySpeed)
    {
        HeadPosition.localPosition = new Vector3(OriginalHeadPosition.x + (HeadRange / MaxVelocity * xSpeed), OriginalHeadPosition.y + (ySpeed > 0 ? BumpLevel : 0), OriginalHeadPosition.z);
        
        var headBehavior = Head.GetComponent<HeadBehavior>();
        if(ySpeed > 0)
        {
            headBehavior.Renderer.sprite = headBehavior.Back;
            headBehavior.HairBack.SetActive(true);
            headBehavior.HairFront.SetActive(false);
        }
        else
        {
            headBehavior.Renderer.sprite = headBehavior.CurrentFace;
            headBehavior.HairBack.SetActive(false);
            headBehavior.HairFront.SetActive(true);
        }

    }
}
