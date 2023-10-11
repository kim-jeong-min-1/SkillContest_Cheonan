using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGauge : MonoBehaviour
{
    [SerializeField] private Text itemName;
    [SerializeField] private GaugeBar gaugeBar;

    public void Init(float time, string name)
    {
        itemName.text = name;
        gaugeBar.Init(time);

        StartCoroutine(GaugeBar(time));
    }

    private IEnumerator GaugeBar(float time)
    {
        float curTime = time;
        float percent = 1;

        while (percent > 0)
        {
            curTime -= Time.deltaTime;
            percent = curTime / time;

            gaugeBar.SetGaugeBar(curTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}
