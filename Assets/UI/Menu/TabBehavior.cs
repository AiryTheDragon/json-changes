using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TabBehavior : MonoBehaviour
{
    bool movingRight = false;
    bool movingLeft = false;
    bool movesRight = true;

    float startTime = 0f;

    float startPos;
    float maxMove = 10f;

    float moveTime = 0.1f;

    bool selected = false;


    // Start is called before the first frame update
    void Start()
    {
        startPos = GetComponent<Transform>().position.x;
    }

    private void ResetPosition()
    {
        Vector3 pos = GetComponent<Transform>().position;
        pos = new Vector3(startPos, pos.y, pos.z);
        GetComponent<Transform>().position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(IsPointerOverUIElement());
        if(movingRight)
        {

            float dif = Time.time - startTime;
            Vector3 currentPosition = GetComponent<Transform>().position;
            if(dif >= moveTime || dif <= 0)
            {
                movingRight = false;
                if(movesRight)
                {
                    currentPosition = new Vector3(startPos + maxMove, currentPosition.y, currentPosition.z);
                }
                else
                {
                    currentPosition = new Vector3(startPos, currentPosition.y, currentPosition.z);
                }
            }
            else
            {
                if(movesRight)
                {
                    currentPosition = new Vector3(startPos + maxMove / moveTime * (Time.time - startTime), currentPosition.y, currentPosition.z);
                }
                else
                {
                    currentPosition = new Vector3(startPos - maxMove + maxMove / moveTime * (Time.time - startTime),
                                                    currentPosition.y, currentPosition.z);
                }
            }
            GetComponent<Transform>().position = currentPosition;
            
        }
        else if(movingLeft)
        {
            Vector3 currentPosition = GetComponent<Transform>().position;
            float dif = Time.time - startTime;
            if(dif >= moveTime || dif <= 0)
            {
                movingLeft = false;
                if(movesRight)
                {
                    currentPosition = new Vector3(startPos, currentPosition.y, currentPosition.z);
                }
                else
                {
                    currentPosition = new Vector3(startPos - maxMove, currentPosition.y, currentPosition.z);
                }
            }
            else
            {
                if(movesRight)
                {
                    currentPosition = new Vector3(startPos + maxMove - maxMove / moveTime * (Time.time - startTime),
                                                    currentPosition.y, currentPosition.z);
                }
                else
                {
                    currentPosition = new Vector3(startPos - maxMove / moveTime * (Time.time - startTime),
                                                    currentPosition.y, currentPosition.z);
                }
            }
            GetComponent<Transform>().position = currentPosition;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //mouse_over = true;
        Debug.Log("Mouse enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //mouse_over = false;
        Debug.Log("Mouse exit");
    }
/*
    void OnMouseOver()
    {
        Debug.Log("Test");
        if(!selected)
        {
            if(moveTime < Time.time - startTime)
            {
                movingLeft = false;
                movingRight = false;
                ResetPosition();
            }
            else
            {
                if(movesRight)
                {
                    movingRight = true;
                    movingLeft = false;
                }
                else
                {
                    movingRight = false;
                    movingLeft = true;
                }
            }
        }
    }

    void OnMouseExit()
    {
        if(!selected)
        {
            if(moveTime < Time.time - startTime)
            {
                movingLeft = false;
                movingRight = false;
                ResetPosition();
            }
            else
            {
                if(movesRight)
                {
                    movingLeft = true;
                    movingRight = false;
                }
                else
                {
                    movingLeft = false;
                    movingRight = true;
                }
            }
        }
    }
    */
}
