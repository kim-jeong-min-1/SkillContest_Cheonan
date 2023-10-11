using JM.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WaveNotifi : MonoBehaviour
{
    [SerializeField] private RectTransform rect;
    [SerializeField] private Text waveText;
    [SerializeField] private float moveTime;

    public void Notifi(int waveIndex)
    {
        StartCoroutine(waveNotifi());
        IEnumerator waveNotifi()
        {
            waveText.text = $"Wave. {waveIndex} Ω√¿€!";

            yield return rect.DoAnchorMove(new Vector2(0, 30), moveTime, Ease.OutElastic);
            yield return new WaitForSeconds(0.12f);
            yield return rect.DoAnchorMove(new Vector2(0, -1000), moveTime, Ease.InElastic);
        }
    }
}
