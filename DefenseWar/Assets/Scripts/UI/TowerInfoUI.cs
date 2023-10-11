using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TowerInfoUI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private TowerInfo towerInfo;
    [SerializeField] private Image image;
    [SerializeField] private Text priceText;
    [SerializeField] private Text nameText;
    private int price;

    void Awake() => Init(towerInfo);
    private void Init(TowerInfo info)
    {
        image.sprite = info.towerImage;
        price = info.price;
        priceText.text = $"Price : {price}";
        nameText.text = info.towerName;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!GameManager.Inst.EnoughBuildCheck())
        {
            GameManager.Inst.Red_Notifi(Utils.NotEnoughBuild);
            return;
        }
        else if (!GameManager.Inst.EnoughGoldCheck(price))
        {
            GameManager.Inst.Red_Notifi(Utils.NotEnoughGold);
            return;
        }
        GameManager.Inst.BuildTower(towerInfo);
    }

}
