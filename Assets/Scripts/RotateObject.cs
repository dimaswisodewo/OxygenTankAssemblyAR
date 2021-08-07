using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private void OnEnable()
    {
        ObjectManipulation.onRotate += OnRotate;
    }

    private void OnDisable()
    {
        ObjectManipulation.onRotate -= OnRotate;
    }

    public void OnRotate(float rotateValue)
    {
        transform.Rotate(Vector3.up, rotateValue, Space.Self);
    }
}
