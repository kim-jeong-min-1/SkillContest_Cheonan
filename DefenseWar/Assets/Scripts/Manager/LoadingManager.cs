using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private float minLoadingTime;
    [SerializeField] private float maxLoadingTime;  
    [SerializeField] private float minLoadValue;
    [SerializeField] private float maxLoadValue;
    [SerializeField] private Text loadText;
    [SerializeField] private GaugeBar loadBar;
    private int dot = 0;

    private void Start()
    {
        StartCoroutine(Loading());
    }

    private IEnumerator Loading()
    {
        var loadTime = Random.Range(minLoadingTime, maxLoadingTime);
        loadBar.Init(loadTime);
        var curTime = 0f;

        while (curTime < loadTime)
        {
            var randTime = Random.Range(minLoadValue, maxLoadValue);

            curTime += randTime;
            loadBar.SetGaugeBar(curTime);

            if(dot == 3)
            {
                dot = 0;
                loadText.text = "Loading";
            }
            loadText.text = loadText.text + ".";
            dot++;

            yield return new WaitForSeconds(0.1f);
        }

        SceneLoadManager.Inst.LoadScene("Stage1");
    }
}
