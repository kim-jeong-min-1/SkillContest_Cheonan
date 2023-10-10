using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    [SerializeField] private EntityStat entityStats;
    [SerializeField] private GaugeBar entityHpBar;
    [SerializeField] private Transform center;
    private float maxHp;
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

    protected virtual void Awake()
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
        targetLayer = Utils.GetTargetLayer(stats.attackType);
        StartCoroutine(EntityUpdateTarget());
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
            yield return new WaitForSeconds(0.05f);
        }
    }

    protected virtual void Die()
    {

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

    protected virtual void OnDrawGizmos()
    {
        if (entityStats.attackType == EntityAttackType.NonAttack) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(center.position, viewRange);
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
