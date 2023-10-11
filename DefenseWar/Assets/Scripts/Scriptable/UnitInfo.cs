using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="UnitInfoUI", menuName ="UnitInfo")]
public class UnitInfo : ScriptableObject
{
    public Unit unitObj;
    public Sprite unitImage;
    public string unitName;
    public int price;
}
