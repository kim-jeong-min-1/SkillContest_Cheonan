using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JM.Tweening;

public class RedNotifi : MonoBehaviour
{
    [SerializeField] private RectTransform rect;
    [SerializeField] private Text notifi;
    [SerializeField] private float moveTime;

    public void OnNotifi(string notifi)
    {
        this.notifi.text = notifi;
        StartCoroutine(onNotifi());

        IEnumerator onNotifi()
        {
            yield return rect.DoAnchorMove(new Vector2(0, 440), moveTime, Ease.OutQuad);
            yield return new WaitForSeconds(0.25f);
            yield return rect.DoAnchorMove(new Vector2(0, 650), moveTime, Ease.InQuad);
        }
    }
}
