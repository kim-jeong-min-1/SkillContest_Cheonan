using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector3 point { get; private set; }
    public TileType tileType;

    public void Init()
    {
        point = transform.position;
    }
}

public enum TileType
{
    Empty,
    Fill
}
