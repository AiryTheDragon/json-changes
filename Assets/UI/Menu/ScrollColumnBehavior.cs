using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ScrollColumnBehavior : ButtonBase
{
    public int Rows;

    public UnityEvent<int> OnClick;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {

    }

    public override void OnPointerExit(PointerEventData eventData)
    {

    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        var location = GetComponent<RectTransform>();
        float pressY = eventData.pressPosition.y - location.position.y;
        int index = (int)(pressY / location.sizeDelta.y * Rows);
        OnClick?.Invoke(index);
    }
}
