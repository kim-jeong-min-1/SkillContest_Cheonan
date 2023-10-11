using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeInfoUI : MonoBehaviour
{
    [SerializeField] private Text priceText;
    [SerializeField] private Text nameText;
    [SerializeField] private Text countText;
    [SerializeField] private string name;
    [SerializeField] private int price;
    [SerializeField] private int priceUpValue;
    [SerializeField] private int maxUpgrade;
    private int curUpgrade = 0;
    public System.Action action { get; set; }

    private void Awake()
    {
        Init();
    }
    public void Init()
    {
        nameText.text = name;
        priceText.text = $"Price : {price}";
        countText.text = $"구매가능 횟수 : {curUpgrade} / {maxUpgrade}";
    }

    public void Upgrade()
    {
        if (GameManager.Inst.EnoughGoldCheck(price))
        {
            if(curUpgrade >= maxUpgrade)
            {
                GameManager.Inst.Red_Notifi(Utils.BuyCountMax);
                return;
            }
            action();
        }
        else
        {
            GameManager.Inst.Red_Notifi(Utils.NotEnoughGold);
        }

        Init();
    }

    public void Buy()
    {
        price += priceUpValue;
        curUpgrade++;
        GameManager.Inst.Gold -= price;
    }
}
