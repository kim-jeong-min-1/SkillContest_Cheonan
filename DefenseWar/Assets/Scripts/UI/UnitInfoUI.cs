using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitInfoUI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private UnitInfo unitInfo;
    [SerializeField] private Image image;
    [SerializeField] private Text priceText;
    [SerializeField] private Text nameText;
    private int price;

    void Awake() => Init(unitInfo);
    private void Init(UnitInfo info)
    {
        image.sprite = info.unitImage;
        price = info.price;
        priceText.text = $"Price : {price}";
        nameText.text = info.unitName;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!GameManager.Inst.EnoughPeopleCheck() || !GameManager.Inst.EnoughGoldCheck(price))
        {
            //¾Ë¶÷
            return;
        }

        GameManager.Inst.SpawnUnit(unitInfo);
    }

}
