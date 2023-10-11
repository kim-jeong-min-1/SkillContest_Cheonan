using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyUnit
{
    protected override void Die()
    {
        base.Die();
        GameManager.Inst.isGameClear = true;
    }
}
