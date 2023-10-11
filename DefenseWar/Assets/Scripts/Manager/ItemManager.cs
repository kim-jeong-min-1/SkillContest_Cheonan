using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : Singleton<ItemManager>
{
    [SerializeField] private UpgradeInfoUI itemUpgrade;
    [SerializeField] private Transform itemGrid;
    [SerializeField] private Transform gaugeGrid;
    [SerializeField] private List<Item> items;
    [SerializeField] private ItemGauge itemGauge;
    [SerializeField] private Image cool;
    private Dictionary<ItemType, Item> curItmeDic = new();

    private bool isCool = false;
    private float coolTime = 5f;
    private readonly int maxCount = 3;

    protected override void Awake()
    {
        base.Awake();
        itemUpgrade.action += RandItem;
    }

    public void RandItem()
    {
        if(itemGrid.childCount >= maxCount)
        {
            GameManager.Inst.Red_Notifi(Utils.ItmeMax);
            return;
        }

        int rand;
        while (true)
        {
            rand = Random.Range(0, items.Count);
            if (!curItmeDic.ContainsKey((ItemType)rand)) break;
        }

        var item = rand switch
        {
            0 => items[0],
            1 => items[1],
            2 => items[2],
            3 => items[3],
            4 => items[4],
            5 => items[5],
        };

        itemUpgrade.Buy();
        curItmeDic.Add((ItemType)rand, item);
        GameManager.Inst.Red_Notifi(item.itemName);
        Instantiate(item, itemGrid);
    }

    public void UseItem(Item item)
    {
        if (!isCool)
        {
            switch(item.type)
            {
                case ItemType.TowerHpUp:
                    GameManager.Inst.TowerHeal(GameManager.Inst.curTowers);
                    break;
                case ItemType.AllEnemySlow:
                    GameManager.Inst.EnemySpeedDown(GameManager.Inst.curEnemyUnits);
                    break;
                case ItemType.GetGoldUp:
                    GameManager.Inst.GoldUP();
                    break;
                case ItemType.TowerAttackSpeed:
                    GameManager.Inst.TowerAttackSpeedUp(GameManager.Inst.curTowers);
                    break;
                case ItemType.EnemyStop:
                    GameManager.Inst.EnemyAttackEnable(GameManager.Inst.curEnemyUnits);
                    break;
                case ItemType.InvicibleUnitSpawn:
                    GameManager.Inst.SpawnInvincibleUnit();
                    break;
            }
            isCool = true;
            StartCoroutine(CoolTime());

            var gauge = Instantiate(itemGauge, gaugeGrid);
            gauge.Init(item.duration, item.itemName);

            GameManager.Inst.Red_Notifi(item.itemName);
            Destroy(item.gameObject);
        }
    }

    private IEnumerator CoolTime()
    {
        cool.gameObject.SetActive(true);
        float curTime = 0;
        float percent = 0;
        while (percent < 1)
        {
            curTime += Time.deltaTime;
            percent = curTime / coolTime;

            cool.fillAmount = percent;
            yield return null;
        }
        cool.gameObject.SetActive(false);
        isCool = false;
    }
}

public enum ItemType
{
    TowerHpUp,
    AllEnemySlow,
    GetGoldUp,
    TowerAttackSpeed,
    EnemyStop,
    InvicibleUnitSpawn
}

