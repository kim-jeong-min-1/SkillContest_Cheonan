using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITab : MonoBehaviour
{
    [SerializeField] private List<GameObject> tabs;
    
    public void ChangeTab(int index)
    {
        for (int i = 0; i < tabs.Count; i++)
        {
            if (i == index) tabs[i].SetActive(true);
            else
            {
                tabs[i].SetActive(false);
            }
        }
    }
}
