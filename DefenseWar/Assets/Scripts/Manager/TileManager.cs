using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : Singleton<TileManager>
{
    public Dictionary<Vector3, Tile> tiles = new();

    protected override void Awake()
    {
        base.Awake();

        foreach (Transform tile in transform)
        {
            var tileObj = tile.GetComponent<Tile>();

            tileObj.Init();
            AddTile(tileObj);
        }
    }


    public void AddTile(Tile tile)
    {
        tiles.Add(tile.point, tile);
    }

    public Tile GetTile(Vector3 pos)
    {
        if (!tiles.ContainsKey(pos)) return null;

        return tiles[pos];
    }
}
