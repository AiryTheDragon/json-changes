using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Linq;

public class ContinueButtonBehavior : ButtonBase
{
    private MenuBehavior menu;

    void Start()
    {
        menu = Resources.FindObjectsOfTypeAll<MenuBehavior>().First();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<TextMeshProUGUI>().color = Color.gray;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<TextMeshProUGUI>().color = Color.black;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        GetComponent<TextMeshProUGUI>().color = Color.black;
        menu?.Close();
    }
}