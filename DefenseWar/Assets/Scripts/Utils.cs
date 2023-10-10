using UnityEngine;

public static class Utils 
{
    public static LayerMask GetTargetLayer(EntityAttackType type)
    {
        var layer = type switch
        {
            EntityAttackType.NonAttack => LayerMask.GetMask("NonAttack"),
            EntityAttackType.PlayerAttack_Ground => LayerMask.GetMask("EnemyUnit_Ground"),
            EntityAttackType.PlayerAttack_Multi => LayerMask.GetMask("EnemyUnit_Ground") | LayerMask.GetMask("EnemyUnit_Fly"),
            EntityAttackType.EnemyAttack_Ground => LayerMask.GetMask("PlayerUnit_Ground") | LayerMask.GetMask("PlayerTower") | LayerMask.GetMask("HeadQuarter"),
            EntityAttackType.EnemyAttack_Multi => LayerMask.GetMask("PlayerUnit_Ground") | LayerMask.GetMask("PlayerTower") | LayerMask.GetMask("HeadQuarter")
            | LayerMask.GetMask("PlayerUnit_Fly"),
            EntityAttackType.EnemyAttack_OnlyHeadQuarter => LayerMask.GetMask("HeadQuarter"),
        };

        return layer;
    }
}
