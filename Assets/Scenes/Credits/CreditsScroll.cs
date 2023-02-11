using System.Collections;
using UnityEngine;

namespace Assets.Scenes.ActualGame
{
    public class CreditsScroll : MonoBehaviour
    {
        public float scrollRate;
        public bool isScrolling = true;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (isScrolling)
            {
                float yChange = scrollRate * Time.deltaTime;
                transform.Translate(0, yChange, 0);
            }
        }
    }
}