using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleObject : MonoBehaviour
{
    private Vector3 _initialScale;

    private void OnEnable()
    {
        _initialScale = transform.localScale;

        ObjectManipulation.onScale += OnScale;
    }

    private void OnDisable()
    {
        ObjectManipulation.onScale -= OnScale;
    }

    public void OnScale(float factor)
    {
        float newScale = _initialScale.x * factor;
        newScale = Mathf.Clamp(newScale, Config.MIN_SCALE, Config.MAX_SCALE);

        transform.localScale = new Vector3(newScale, newScale, newScale);
    }
}
