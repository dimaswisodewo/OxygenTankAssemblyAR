using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARContentManager : MonoBehaviour
{
    public static ARContentManager Instance;

    public AR_CONTENT arContent;
    public LEARNING_CONTENT learningContent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
}

public enum AR_CONTENT
{
    ASSEMBLY,
    DIASSEMBLY
}

public enum LEARNING_CONTENT
{
    SAFETY,
    OXYGEN
}