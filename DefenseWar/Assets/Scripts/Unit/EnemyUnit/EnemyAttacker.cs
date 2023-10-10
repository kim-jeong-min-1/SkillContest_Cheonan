using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private EnemyUnit enemy;

    public void AttackTrigger()
    {
        enemy.GiveDamage();
    }
}
