using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningMaterialData : MonoBehaviour
{
    [HideInInspector]
    public int panelCount;

    private void Awake()
    {
        panelCount = transform.childCount;
    }
}
