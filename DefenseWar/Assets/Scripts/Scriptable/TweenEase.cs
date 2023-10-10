using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TweenEase", menuName ="ScriptableObject/TweenEase")]
public class TweenEase : ScriptableObject
{
    public AnimationCurve[] ease;
}
