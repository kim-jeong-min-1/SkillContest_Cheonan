using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : Entity
{
    [Space(10)][Header("Unit")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform model;
    [SerializeField] private Transform uiArea;
    protected bool isAttackDelay;
    protected float curTime;

    public override void Init(EntityStat stats)
    {
        base.Init(stats);
        agent.speed = speed;

        StartCoroutine(UnitAI());
    }

    protected virtual void Update()
    {
        curTime += Time.deltaTime;
        if(curTime >= attackDelay && isTarget && isCanAttack)
        {
            curTime = 0;
            UnitAttack();
        }
    }

    protected virtual IEnumerator UnitAI()
    {
        while (!isDie)
        {
            if (isTarget)
            {
                var check = Physics.OverlapSphere(GetCenterPos(), attackDamage, targetLayer);
                if (check.Length != 0)
                {
                    isCanAttack = true;
                }
                else
                {
                    isCanAttack = false;
                }

                if (isCanAttack)
                {
                    agent.isStopped = true;
                }
                else
                {
                    agent.isStopped = false;
                    agent.SetDestination(target.transform.position);
                }
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(new Vector3(45, 0, 15));
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    protected virtual void UnitAttack()
    {
        animator.SetTrigger("Attack");        
    }

    public virtual void GiveDamage()
    {
        target.ApplyDamage(attackDamage);
    }
}
