using System.Collections;
using UnityEngine;


public class TutorialPlayer : MonoBehaviour
{

    public TutorialTests testCode;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Writing Desk"))
        {
            testCode.EnterDesk(true);
        }

    }

}