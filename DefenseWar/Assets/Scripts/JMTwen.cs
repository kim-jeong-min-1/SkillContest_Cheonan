using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM.Tweening
{
    public static class JMTwen
    {
        private static Dictionary<Transform, Coroutine> curMoveTweenDic = new();
        private static Dictionary<RectTransform, Coroutine> curAnchorMoveTweenDic = new();

        private static TweenEase tweenEase;
        private static TweenEase TweenEase
        {
            get
            {
                if (tweenEase == null)
                    tweenEase = Resources.Load<TweenEase>("Scriptable/TweenEase");

                return tweenEase;
            }
        }

        public static Coroutine DoMove(this Transform tr, Vector3 target, float time, Ease ease = Ease.None)
        {
            if (curMoveTweenDic.ContainsKey(tr)) return null;

            var tween = tr.GetComponent<MonoBehaviour>().StartCoroutine(tweening());
            curMoveTweenDic.Add(tr, tween);
            return tween;

            IEnumerator tweening()
            {
                float curTime = 0;
                float percent = 0;
                Vector3 start = tr.position;

                AnimationCurve tweenEase = null;
                if (ease != Ease.None)
                {
                    tweenEase = TweenEase.ease[(int)ease];
                }

                while (percent < 1)
                {
                    curTime += Time.deltaTime;
                    percent = curTime / time;

                    var value = (tweenEase != null) ? tweenEase.Evaluate(percent) : percent;

                    tr.position = Vector3.Lerp(start, target, value);
                    yield return null;
                }
                curMoveTweenDic.Remove(tr);
            }
        }

        public static void KillTween(this Transform tr)
        {
            if (!curMoveTweenDic.ContainsKey(tr)) return;

            tr.GetComponent<MonoBehaviour>().StopCoroutine(curMoveTweenDic[tr]);
            curMoveTweenDic.Remove(tr);
        }

        public static Coroutine DoAnchorMove(this RectTransform tr, Vector2 target, float time, Ease ease = Ease.None)
        {
            if (curAnchorMoveTweenDic.ContainsKey(tr)) return null;

            var tween = tr.GetComponent<MonoBehaviour>().StartCoroutine(tweening());
            curAnchorMoveTweenDic.Add(tr, tween);
            return tween;

            IEnumerator tweening()
            {
                float curTime = 0;
                float percent = 0;
                Vector3 start = tr.anchoredPosition;

                AnimationCurve tweenEase = null;
                if(ease != Ease.None)
                {
                    tweenEase = TweenEase.ease[(int)ease];
                }

                while (percent < 1)
                {
                    curTime += Time.deltaTime;
                    percent = curTime / time;

                    var value = (tweenEase != null) ? tweenEase.Evaluate(percent) : percent;

                    tr.anchoredPosition = Vector2.Lerp(start, target, value);
                    yield return null;
                }
                curAnchorMoveTweenDic.Remove(tr);
            }
        }

        public static void KillTween(this RectTransform tr)
        {
            if (!curAnchorMoveTweenDic.ContainsKey(tr)) return;

            tr.GetComponent<MonoBehaviour>().StopCoroutine(curAnchorMoveTweenDic[tr]);
            curAnchorMoveTweenDic.Remove(tr);
        }

    }
}

public enum Ease
{
    Linear,
    OutQuad,
    InQuad,
    None
}

