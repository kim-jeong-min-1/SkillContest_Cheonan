using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : Singleton<Player>
{
    [Header("HeadQuater")]
    [SerializeField] private float nearPointRadius;
    [SerializeField] private List<HeadQuarter> headQuarters;
    public HeadQuarter finalHeadQuarters;

    public Vector3 GetHeadQuarterPos()
    {
        while (true)
        {
            NavMeshHit hit;
            Vector3 pos;

            if (headQuarters.Count != 0)
            {
                var rand = Random.Range(0, headQuarters.Count);
                pos = headQuarters[rand].transform.position + Random.insideUnitSphere * nearPointRadius;
            }
            else
            {
                pos = finalHeadQuarters.transform.position + Random.insideUnitSphere * nearPointRadius;
            }

            if(NavMesh.SamplePosition(pos, out hit, 10f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
    }

    public void ReMoveHeadQuarter(HeadQuarter head)
    {
        headQuarters.Remove(head);
    }
}
