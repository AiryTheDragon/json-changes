using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;

public class Unclickable : MonoBehaviour, IClickable
{

    void Start()
    {

    }


    public void OnMouseUpAsButton()
    {
        // Get a ray from the current mouse position into the game.
        Vector3 mousePos = Input.mousePosition;

        Ray castRay = Camera.main.ScreenPointToRay(mousePos);

        // Cast the ray into the game.
        var hits = Physics.RaycastAll(castRay.origin, castRay.direction, 100, 0, QueryTriggerInteraction.Collide);

        // Get all objects that it hit.
        var myColliders = GetComponents<Collider2D>().Select(x => x.GetInstanceID());

        bool foundCollider = false;
        int hitIndex = 0;

        // Go through until this object's collider.
        while(!foundCollider && hitIndex < hits.Count())
        {
            if(myColliders.Contains(hits[hitIndex].colliderInstanceID))
            {
                foundCollider = true;
                break;
            }
            hitIndex++;
        }

        // Go through until we are past this collider.
        while(foundCollider && hitIndex < hits.Count() && myColliders.Contains(hits[hitIndex].colliderInstanceID))
        {
            hitIndex++;
        }

        // Find the first clickable object.
        while(hitIndex < hits.Count() && hits[hitIndex].collider.GetComponents<IClickable>().Count() < 1)
        {
            hitIndex++;
        }

        // Return if we haven't found anything.
        if(hitIndex >= hits.Count())
        {
            return;
        }

        // Get all the clickables on that object.
        var behaviors = hits[hitIndex].collider.GetComponents<IClickable>();

        // Click them!
        foreach(var behavior in behaviors)
        {
            behavior.OnMouseUpAsButton();
        }
    }
}
