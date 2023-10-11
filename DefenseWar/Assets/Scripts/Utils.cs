using UnityEngine;

public static class Utils 
{
    public static float MAX_CAMERA_X = 57f;
    public static float MIN_CAMERA_X = 30f;
    public static float MAX_CAMERA_Z = 20f;
    public static float MIN_CAMERA_Z = -0.3f;

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

    public static string NotEnoughGold = "��尡 �����մϴ�!";
    public static string NotEnoughPeople = "�α� ���� �� á���ϴ�!";
    public static string NotEnoughBuild = "�ǹ� ���� �� á���ϴ�!";
    public static string NotBuildHere = "�̰����� ��ġ�� �� �����ϴ�!";
}
