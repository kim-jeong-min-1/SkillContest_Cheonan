using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Entity
{
    [Space(10)]
    [Header("Tower")]
    [SerializeField] private List<MeshRenderer> renders;
    [SerializeField] private List<CheckTile> checks;
    [SerializeField] private Transform rot;
    [SerializeField] protected Transform firePos;
    [SerializeField] protected ParticleSystem effect;
    [SerializeField] protected GameObject bullet;
    [SerializeField] private Animator animator;
    [SerializeField] private float buildTime;
    private List<Tile> tiles = new();
    private float curTime;
    private bool isBuild;

    protected override void Start()
    {
       
    }

    public virtual void TowerInit()
    {
        transform.GetComponent<Collider>().enabled = true;

        checks.ForEach(c => tiles.Add(c.curTile));
        checks.ForEach(c => Destroy(c.gameObject));
        tiles.ForEach(t => t.tileType = TileType.Fill);
        base.Start();
    }
    public override void Init(EntityStat stats)
    {
        base.Init(stats);
        StartCoroutine(BuildIn());
    }

    protected virtual void Update()
    {
        curTime += Time.deltaTime;
        if (curTime >= attackDelay && isTarget && isBuild)
        {
            curTime = 0;
            TowerAttack();
        }
    }

    private IEnumerator BuildIn()
    {
        float curTime = buildTime;
        float percent = 1;
        while (percent > 0)
        {
            curTime -= Time.deltaTime;
            percent = curTime / buildTime;

            renders.ForEach(r => r.material.SetFloat("_Amount", percent));
            yield return null;
        }
        isBuild = true;
    }

    private IEnumerator Break()
    {
        float curTime = 0;
        float percent = 0;
        while (percent < 1)
        {
            curTime += Time.deltaTime;
            percent = curTime / buildTime;

            renders.ForEach(r => r.material.SetFloat("_Amount", percent));
            yield return null;
        }
        Destroy(gameObject);
        tiles.ForEach(t => t.tileType = TileType.Empty);
    }

    protected virtual void TowerAttack()
    {
        animator.SetTrigger("Attack");

        var dir = (target.GetCenterPos() - rot.position).normalized;
        rot.rotation = Quaternion.LookRotation(dir, Vector3.up);
    }

    public virtual void GiveDamage()
    {
        if (target == null) return;
        target.ApplyDamage(attackDamage);
    }

    public bool BuildCheck()
    {
        foreach (var check in checks)
        {
            if (!check.isCheck) return false;
        }
        return true;
    }

    protected override void Die()
    {
        base.Die();
        isBuild = false;
        StartCoroutine(Break());
    }
}
