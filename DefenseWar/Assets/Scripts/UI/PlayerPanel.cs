using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JM.Tweening;

public class PlayerPanel : MonoBehaviour
{
    [SerializeField] private RectTransform rect;
    [SerializeField] private GameObject down;
    [SerializeField] private GameObject up;
    [SerializeField] private float moveTime;

    public void Movement()
    {
        if (up.activeSelf)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }

    public void MoveUp()
    {
        rect.DoAnchorMove(new Vector2(0, 0), moveTime, Ease.OutQuad);

        up.SetActive(false);
        down.SetActive(true);
    }

    public void MoveDown()
    {
        rect.DoAnchorMove(new Vector2(0, -295), moveTime, Ease.OutQuad);

        up.SetActive(true);
        down.SetActive(false);
    }
}
