using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marine : Unit
{
    [Space(10)][Header("Marine")]
    [SerializeField] private LineRenderer line;
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject effect;
   
    public override void GiveDamage()
    {
        base.GiveDamage();
        StartCoroutine(AttackEffect());
    }

    private IEnumerator AttackEffect()
    {
        if (target == null) yield break;

        line.gameObject.SetActive(true);
        effect.SetActive(true);

        line.SetPosition(0, firePos.position);
        line.SetPosition(1, target.GetCenterPos());

        yield return new WaitForSeconds(0.1f);

        effect.SetActive(false);
        line.gameObject.SetActive(false);
    }
}
