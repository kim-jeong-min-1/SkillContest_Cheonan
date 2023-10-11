using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public ItemType type;
    public string itemName;
    public float duration;
    [SerializeField] private GameObject explainPopup;

    public void OnPointerDown(PointerEventData eventData)
    {
        ItemManager.Inst.UseItem(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        explainPopup.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        explainPopup.SetActive(false);
    }
}
