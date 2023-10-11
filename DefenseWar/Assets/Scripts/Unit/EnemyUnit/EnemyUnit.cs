using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : Unit
{
    [Space(10)][Header("Enemy")]
    [SerializeField] private int score;
    [SerializeField] private int gold;

    protected override void Die()
    {
        GameManager.Inst.Gold += gold;
        GameManager.Inst.Score += score;
        base.Die();
    }
}
