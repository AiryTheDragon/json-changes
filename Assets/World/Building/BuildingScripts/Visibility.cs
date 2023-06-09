using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visibility : MonoBehaviour
{
    [SerializeField] Color32 showObject = new Color32(255, 255, 255, 255);
    [SerializeField] Color32 noShowObject = new Color32(127, 127, 127, 128);


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Ceiling")
        {
            SpriteRenderer[] spriteRenderers = collider.GetComponentsInChildren<SpriteRenderer>();
            //spriteRenderer.color = noShowObject;
            foreach(SpriteRenderer spriteRenderer in spriteRenderers)
            {
                spriteRenderer.enabled = false;
            }
        }

    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Ceiling")
        {
            SpriteRenderer[] spriteRenderers = collider.GetComponentsInChildren<SpriteRenderer>();
            //spriteRenderer.color = noShowObject;
            foreach(SpriteRenderer spriteRenderer in spriteRenderers)
            {
                spriteRenderer.enabled = true;
            }
        }

    }

}
