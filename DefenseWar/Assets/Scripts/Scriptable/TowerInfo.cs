using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="TowerInfo", menuName = "TowerInfo")]
public class TowerInfo : ScriptableObject
{
    public Tower towerObj;
    public Sprite towerImage;
    public string towerName;
    public int price;
    public bool isEven;
}