using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeBar : MonoBehaviour
{
    [SerializeField] private Image gauge;
    private float maxGauge;
    
    public void Init(float _maxGauge)
    {
        maxGauge = _maxGauge;
        gauge.fillAmount = maxGauge / maxGauge;
    }

    public void SetGaugeBar(float curGauge)
    {
        gauge.fillAmount = curGauge / maxGauge;
    }
}
