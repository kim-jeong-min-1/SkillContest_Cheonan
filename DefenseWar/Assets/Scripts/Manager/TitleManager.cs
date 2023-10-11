using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public void StartButton()
    {
        SceneLoadManager.Inst.LoadScene("Loading");
    }
}
