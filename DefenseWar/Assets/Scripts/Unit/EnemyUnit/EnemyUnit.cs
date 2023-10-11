using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : Unit
{
    [Space(10)][Header("Enemy")]
    [SerializeField] private int score;
    [SerializeField] private int gold;

    public bool isCantAttack { get; set; }

    protected override void Die()
    {
        GameManager.Inst.Gold += gold;
        GameManager.Inst.Score += score;
        base.Die();
    }

    public void WaveEndDie()
    {
        base.Die();
    }

    protected override void Update()
    {
        curTime += Time.deltaTime;
        if (curTime >= attackDelay && isTarget && isCanAttack && !isCantAttack)
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

    public void EnemySpeedDown()
    {
        agent.speed = speed * 0.5f;
    }

    public void EnemySpeedUp()
    {
        agent.speed = speed;
    }
}
