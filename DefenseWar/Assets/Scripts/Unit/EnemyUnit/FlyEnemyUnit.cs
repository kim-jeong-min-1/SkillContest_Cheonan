using JM.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyUnit : EnemyUnit
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePos;
    [SerializeField] private float moveTime;

    public override void GiveDamage()
    {
        if (target == null) return;
        StartCoroutine(ShotBullet());
    }

    private IEnumerator ShotBullet()
    {
        bullet.SetActive(true);
        bullet.transform.position = firePos.position;
        LookTarget(bullet.transform, target.GetCenterPos());

        yield return bullet.transform.DoMove(target.GetCenterPos(), moveTime, Ease.OutQuad);

        bullet.SetActive(false);
        base.GiveDamage();
    }
}
