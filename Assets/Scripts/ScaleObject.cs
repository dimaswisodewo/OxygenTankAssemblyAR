using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleObject : MonoBehaviour
{
    private Vector3 _initialScale;
    private float _smoothing = 0.5f;

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
        newScale = Mathf.Lerp(transform.localScale.x, newScale, _smoothing);
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }
}
