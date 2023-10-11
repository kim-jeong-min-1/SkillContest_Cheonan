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
        if (!GameManager.Inst.EnoughPeopleCheck())
        {
            GameManager.Inst.Red_Notifi(Utils.NotEnoughPeople);
            return;
        }
        else if (!GameManager.Inst.EnoughGoldCheck(price))
        {
            GameManager.Inst.Red_Notifi(Utils.NotEnoughGold);
            return;
        }

        GameManager.Inst.People++;
        GameManager.Inst.Gold -= price;
        GameManager.Inst.SpawnUnit(unitInfo);
    }

}
