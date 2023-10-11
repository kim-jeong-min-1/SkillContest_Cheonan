using JM.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;
using UnityEngine.UI;

public class WaveManager : Singleton<WaveManager>
{
    [SerializeField] private List<EnemyUnit> enemies;
    [SerializeField] private EnemyUnit curStageBoss;
    [SerializeField] private WaveNotifi waveNotifi;
    [SerializeField] private GaugeBar waveProcess;
    [SerializeField] private RectTransform waveWait;
    [SerializeField] private Text waitText;
    [SerializeField] private Text waveText;
    [Space(10)]
    [SerializeField] private int maxWave;
    [SerializeField] private int waitTime;
    [SerializeField] private int waveDuration;
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;
    [SerializeField] private float spawnTimeValue;
    private List<Transform> spawnPoints = new();
    private int curWave = 0;
    private int maxIndex = 2;
    private bool isWaiting = true;

    protected override void Awake()
    {
        base.Awake();

        waveProcess.Init(waveDuration);
        foreach (Transform points in transform)
        {
            spawnPoints.Add(points);
        }
        StartCoroutine(Wave());
    }

    private IEnumerator Wave()
    {
        while (curWave < maxWave)
        {
            curWave++;
            isWaiting = true;
            waveProcess.SetGaugeBar(waveDuration);

            yield return WaveWaiting();
            waveWait.DoAnchorMove(new Vector2(295, 0), 1f, Ease.OutQuad);

            waveNotifi.Notifi(curWave);
            waveText.text = $"Wave : {curWave} / {maxWave}";

            if (curWave % 4 == 0) maxIndex++;
            yield return StartCoroutine(WaveUpdate(maxIndex));

            GameManager.Inst.WaveEnd();
            minSpawnTime -= spawnTimeValue;
            maxSpawnTime -= spawnTimeValue;
        }  
    }

    private IEnumerator WaveWaiting()
    {
        float curTime = 0;
        waveWait.DoAnchorMove(new Vector2(0, 0), 1f, Ease.OutQuad);
        while (isWaiting)
        {
            curTime += Time.deltaTime;
            waitText.text = $"{(int)curTime / 60} : {(int)curTime % 60} / 2 : 00";

            if(curTime >= waitTime)
            {
                isWaiting = false;
                break;
            }
            yield return null; 
        }
    }

    private IEnumerator WaveUpdate(int maxIndex)
    {
        float curTime = 0;
        while (curTime < waveDuration)
        {
            var randEnemy = Random.Range(0, maxIndex);
            var randPos = Random.Range(0, spawnPoints.Count);
            var randWaitTime = Random.Range(minSpawnTime, maxSpawnTime);

            var enemy = Instantiate(enemies[randEnemy], spawnPoints[randPos].position, Quaternion.identity);
            GameManager.Inst.curEnemyUnits.Add(enemy);

            curTime += randWaitTime;
            waveProcess.SetGaugeBar(curTime);
            yield return new WaitForSeconds(randWaitTime);
        }
    }

    public void NonWaiting()
    {
        isWaiting = false;
    }
}
