using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T inst;
    public static T Inst
    {
        get
        {
            if(inst == null)
            {
                inst = FindObjectOfType<T>();
            }
            return inst;
        }
    }
    private void Init()
    {
        if(inst == null)
        {
            inst = FindObjectOfType<T>();
        }
        else
        {
            Destroy(gameObject);
        }

    }

    protected virtual void Awake()
    {
        Init();
    }
}
