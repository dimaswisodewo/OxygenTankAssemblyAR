using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VersioningText : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Text>().text = Config.APP_VERSION;
    }
}
