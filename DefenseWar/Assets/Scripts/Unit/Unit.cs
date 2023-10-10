using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : Entity
{
    [Space(10)]
    [Header("Unit")]
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
        agent.updateRotation = false;
        curTime = attackDelay;

        StartCoroutine(UnitAI());
    }

    protected virtual void Update()
    {
        curTime += Time.deltaTime;
        if (curTime >= attackDelay && isTarget && isCanAttack)
        {
            curTime = 0;
            UnitAttack();
        }

        if (!agent.isStopped)
        {
            var foward = new Vector2(transform.position.z, transform.position.x);
            var target = new Vector2(agent.steeringTarget.z, agent.steeringTarget.x);

            var dir = target - foward;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.eulerAngles = Vector3.up * angle;
        }
        uiArea.localEulerAngles = new Vector3(0, -transform.eulerAngles.y, 0);

        animator.SetBool("Walk", !agent.isStopped);
    }

    protected virtual IEnumerator UnitAI()
    {
        while (!isDie)
        {
            if (isTarget)
            {
                var check = Physics.OverlapSphere(GetCenterPos(), attackRange, targetLayer);
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

            yield return null;
        }
    }

    protected override void Die()
    {
        base.Die();
        animator.SetTrigger("Die");

        var time = animator.GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, time);
        enabled = false;
    }

    protected virtual void UnitAttack()
    {
        LookTarget(transform, target.GetCenterPos());
        animator.SetTrigger("Attack");
    }

    public virtual void GiveDamage()
    {
        if (target == null) return;
        target.ApplyDamage(attackDamage);
    }
}
