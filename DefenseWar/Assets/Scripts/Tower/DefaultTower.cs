using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JM.Tweening;

public class DefaultTower : Tower
{
    [SerializeField] private float time;
    public void DefaultTowerAttack()
    {
        StartCoroutine(TowerAttackTrigger());
    }

    private IEnumerator TowerAttackTrigger()
    {
        if (target == null) yield break;

        bullet.transform.position = firePos.position;
        LookTarget(bullet.transform, target.GetCenterPos());
        Instantiate(effect, firePos.position, firePos.rotation);

        bullet.SetActive(true);
        yield return bullet.transform.DoMove(target.GetCenterPos(), time);

        bullet.SetActive(false);
        GiveDamage();
    }
}
