using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadQuarter : Entity
{
    [SerializeField] private List<Transform> unitSpawnPoints;
    public bool isFinal;

    public override void Init(EntityStat stats)
    {
        base.Init(stats);
    }

    protected override void Die()
    {
        base.Die();

        if(isFinal)
        {
            // 게임 오버 처리
            print("GameOver");
        }
        else
        {
            Player.Inst.ReMoveHeadQuarter(this);
        }
    }

    public Vector3 GetRandSpawnPoint()
    {
        var rand = Random.Range(0, unitSpawnPoints.Count);
        return unitSpawnPoints[rand].position;
    }
}
