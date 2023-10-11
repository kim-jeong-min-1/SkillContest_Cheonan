using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    [SerializeField] protected EntityStat entityStats;
    [SerializeField] protected GaugeBar entityHpBar;
    [SerializeField] private Transform center;
    protected float maxHp;
    protected float hp;
    protected float speed;
    protected float attackDamage;
    protected float attackDelay;
    protected float attackRange;
    protected float viewRange;
    protected bool isCanAttack;
    protected bool isTarget;
    protected LayerMask targetLayer;
    protected Entity target;
    public bool isDie { get; private set; }
    public bool isPlayable = false;

    protected virtual void Start()
    {
        Init(entityStats);
    }
    public virtual void Init(EntityStat stats)
    {
        maxHp = stats.hp;
        speed = stats.speed;
        attackDamage = stats.attackDamage;
        attackDelay = stats.attackDelay;
        attackRange = stats.attackRange;
        viewRange = stats.viewRange;

        hp = maxHp;
        entityHpBar.Init(maxHp);

        if (stats.attackType != EntityAttackType.NonAttack)
        {
            targetLayer = Utils.GetTargetLayer(stats.attackType);
            StartCoroutine(EntityUpdateTarget());
        }  
    }

    protected virtual IEnumerator EntityUpdateTarget()
    {
        while (!isDie)
        {
            var check = Physics.OverlapSphere(GetCenterPos(), viewRange, targetLayer);
            if (check.Length != 0)
            {
                var target = GetNearTarget(check);

                if (target)
                {
                    this.target = target;
                    isTarget = true;
                }
            }
            else
            {
                isTarget = false;
            }

            if (target && target.isDie)
            {
                target = null;
                isTarget = false;
            }

            yield return null;
        }
        isTarget = false;
    }

    protected virtual void Die()
    {
        // 더 이상 타겟이 되지 않게
        transform.GetComponent<Collider>().enabled = false;
    }

    public Vector3 GetCenterPos() => center.position;

    public Entity GetNearTarget(Collider[] cols)
    {
        float dis = float.MaxValue;
        Entity target = null;

        foreach (var col in cols)
        {
            var curDis = Vector3.Distance(transform.position, col.transform.position);
            if (dis > curDis)
            {
                dis = curDis;
                target = col.GetComponent<Entity>();
            }
        }

        return target;
    }

    public void ApplyDamage(float damage)
    {
        hp -= damage;
        hp = Mathf.Clamp(hp, 0, maxHp);
        entityHpBar.SetGaugeBar(hp);

        if (hp <= 0 && !isDie)
        {
            isDie = true;
            Die();
        }
    }

    protected void LookTarget(Transform tr, Vector3 target)
    {
        var dir = (target - tr.position).normalized;
        var rot = Quaternion.LookRotation(dir, Vector3.up);

        tr.rotation = rot;
    }

    protected virtual void OnDrawGizmos()
    {
        if (entityStats.attackType == EntityAttackType.NonAttack) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center.position, entityStats.attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(center.position, entityStats.viewRange);
    }
}

[System.Serializable]
public class EntityStat
{
    public float hp;
    public float speed;
    public float attackDamage;
    public float attackDelay;
    public float attackRange;
    public float viewRange;
    public EntityAttackType attackType;
}

public interface IDamageable
{
    public void ApplyDamage(float damage);
}

public enum EntityAttackType
{
    NonAttack,
    PlayerAttack_Ground,
    PlayerAttack_Multi,
    EnemyAttack_Ground,
    EnemyAttack_Multi,
    EnemyAttack_OnlyHeadQuarter
}
