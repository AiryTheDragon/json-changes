using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TabBehavior : ButtonBase
{
    bool movingRight = false;
    bool movingLeft = false;
    public bool movesRight = true;

    float startTime = 0f;

    float startPos;
    public float maxMove = 10f;

    public float moveTime = 0.1f;

    public bool selected = false;

    public GameObject menu;

    bool started;

    // Start is called before the first frame update
    void Start()
    {
        if(!started)
        {
            started = true;
            startPos = GetComponent<Transform>().position.x;
            if(selected)
            {
                OnPointerClick(null);
            }
        }
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
        if(movingRight)
        {

            float dif = Time.time - startTime;
            Vector3 currentPosition = GetComponent<Transform>().position;
            if(dif >= moveTime || dif < 0)
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
                startTime = 0;
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
            if(dif >= moveTime || dif < 0)
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
                startTime = 0;
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

    /*public void OnPointerEnter(PointerEventData eventData)
    {
        //mouse_over = true;
        Debug.Log("Mouse enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //mouse_over = false;
        Debug.Log("Mouse exit");
    }*/

    public override void OnPointerEnter(PointerEventData pointerData)
    {
        if(!selected)
        {
            if(startTime != 0)
            {
                movingLeft = false;
                movingRight = false;
                startTime = 0;
                ResetPosition();
            }
            else
            {
                startTime = Time.time;
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

    public override void OnPointerExit(PointerEventData pointerData)
    {
        if(!selected)
        {
            if(startTime != 0)
            {
                movingLeft = false;
                movingRight = false;
                startTime = 0;
                ResetPosition();
            }
            else
            {
                startTime = Time.time;
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

    public override void OnPointerClick(PointerEventData eventData)
    {
        if(!selected)
        {
            GetComponentInParent<MenuBehavior>().SelectTab(this);
        }
    }

    public void Select()
    {
        if(!started)
        {
            Start();
        }
        selected = true;
        Vector3 currentPosition = GetComponent<Transform>().position;
        if(movesRight)
        {
            currentPosition = new Vector3(startPos + maxMove, currentPosition.y, currentPosition.z);
        }
        else
        {
            currentPosition = new Vector3(startPos - maxMove, currentPosition.y, currentPosition.z);
        }
        GetComponent<Transform>().position = currentPosition;
        if(menu != null)
        {
            menu.SetActive(true);
        }
    }

    public void Deselect()
    {
        if(!started)
        {
            Start();
        }
        selected = false;
        Vector3 currentPosition = GetComponent<Transform>().position;
        currentPosition = new Vector3(startPos, currentPosition.y, currentPosition.z);
        GetComponent<Transform>().position = currentPosition;
        startTime = Time.time;
        if(movesRight)
        {
            movingLeft = true;
            movingRight = false;
        }
        else
        {
            movingRight = true;
            movingLeft = false;
        }
        if(menu != null)
        {
            menu.SetActive(false);
        }
    }
}
