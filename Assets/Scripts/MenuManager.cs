using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _subMenus;

    private void Awake()
    {
        foreach (GameObject subMenu in _subMenus)
        {
            subMenu.SetActive(false);
        }
    }

    public void SetARContent(int index)
    {
        ARContentManager.Instance.arContent = (AR_CONTENT)index;
    }

    public void SetLearningContent(int index)
    {
        ARContentManager.Instance.learningContent = (LEARNING_CONTENT)index;
    }
}
