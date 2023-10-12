using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JM.Tweening;

public class GameResultUI : MonoBehaviour
{
    [SerializeField] private GameObject fade;
    [SerializeField] private GameObject clear;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private RectTransform popUp;
    [SerializeField] private Text scoreText;
    [SerializeField] private List<Image> starImages;
    [SerializeField] private float moveTime;

    public void OnResult(bool gameResult)
    {
        WaveManager.Inst.enabled = false;

        fade.SetActive(true);
        popUp.DoAnchorMove(new Vector2(0, 15), moveTime, Ease.OutQuad);
        StartCoroutine(ScoreTexting(GameManager.Inst.Score));

        if (gameResult)
        {
            clear.SetActive(true);
            StartCoroutine(StartRoutine(3));
        }
        else
        {
            gameOver.SetActive(true);
            StartCoroutine(StartRoutine(1));
        }
    }

    public void NextButton()
    {
        popUp.DoAnchorMove(new Vector2(0, -1000), moveTime, Ease.OutQuad);
        SceneLoadManager.Inst.LoadScene("Rank", SceneLoadManager.Inst.RankInput);
        
        // 스테이지 2 추가시에
        //if (GameManager.Inst.curStageIndex == 1)
        //{
        //    SceneLoadManager.Inst.LoadScene("Stage2");
        //}
        //else
        //{
        //    SceneLoadManager.Inst.LoadScene("Rank");
        //}
    }

    private IEnumerator ScoreTexting(int score)
    {
        var curScore = 0;
        while (curScore < score)
        {
            curScore += score / 100;

            if(curScore == 0)
            {
                break;
            }

            scoreText.text = $"SCORE : {curScore}";
            yield return null;
        }

        curScore += score % 100;
        scoreText.text = $"SCORE : {curScore}";
    }

    private IEnumerator StartRoutine(int index)
    {
        for (int i = 0; i < index; i++)
        {
            float curTime = 0;
            float percent = 0;
            while (percent < 1)
            {
                curTime += Time.deltaTime;
                percent = curTime / 1f;

                starImages[i].fillAmount = percent;
                yield return null;
            }
        }
    }
 }
