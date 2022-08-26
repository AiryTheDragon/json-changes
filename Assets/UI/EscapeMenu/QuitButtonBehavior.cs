using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using System.Linq;

public class QuitButtonBehavior : ButtonBase
{
    

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
        SceneManager.LoadScene("StartScene");
    }
}