using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTile : MonoBehaviour
{
    [SerializeField] private GameObject sucess;
    [SerializeField] private GameObject fail;
    public Tile curTile { get; private set; }
    public bool isCheck { get; private set; }

    private void Start()
    {
        StartCoroutine(Checking());
    }

    private IEnumerator Checking()
    {
        while (gameObject)
        {
            var checkPos = transform.position;
            var tile = TileManager.Inst.GetTile(checkPos);

            if(tile == null || tile.tileType == TileType.Fill)
            {
                sucess.SetActive(false);
                fail.SetActive(true);
                isCheck = false;
            }
            else
            {
                sucess.SetActive(true);
                fail.SetActive(false);
                isCheck = true;
            }

            if (tile != null) curTile = tile;

            yield return new WaitForSeconds(0.05f);
        }
    }
}
