using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public void StartButton()
    {
        SceneLoadManager.Inst.LoadScene("Loading");
    }

    public void RankButton()
    {
        SceneLoadManager.Inst.LoadScene("Rank");
    }

    public void ExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
