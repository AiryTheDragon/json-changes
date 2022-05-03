using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public virtual void OnPointerEnter(PointerEventData eventData)
    {

    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {

    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {

    }
}