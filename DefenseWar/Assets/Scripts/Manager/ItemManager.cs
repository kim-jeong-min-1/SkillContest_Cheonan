using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    [SerializeField] private Transform itemGrid;
    [SerializeField] private List<Item> items;
    private Dictionary<ItemType, Item> curItmeDic = new();

    private readonly int maxCount = 3;

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
        curItmeDic.Add((ItemType)rand, item);
        Instantiate(item, itemGrid);
    }

    public void UseItem()
    {

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
