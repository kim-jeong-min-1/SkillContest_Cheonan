using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : Singleton<SceneLoadManager>
{
    [SerializeField] private GameObject circle;
    [SerializeField] private float loadTime;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string name)
    {
        StartCoroutine(LoadScene());
        IEnumerator LoadScene()
        {
            yield return StartCoroutine(FadeIn());
            SceneManager.LoadScene(name);
            yield return StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeIn()
    {
        var start = Vector3.one;
        var target = Vector3.zero;

        float curTime = 0;
        float percent = 0;
        while (percent < 1)
        {
            curTime += Time.deltaTime;
            percent = curTime / loadTime;
            circle.transform.localScale = Vector3.Lerp(start, target, percent);
            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        var start = Vector3.zero;
        var target = Vector3.one;

        float curTime = 0;
        float percent = 0;
        while (percent < 1)
        {
            curTime += Time.deltaTime;
            percent = curTime / loadTime;
            circle.transform.localScale = Vector3.Lerp(start, target, percent);
            yield return null;
        }
    }
}
