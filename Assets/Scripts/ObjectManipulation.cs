using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManipulation : MonoBehaviour
{
    [SerializeField]
    private bool _canBeRotated = true;

    [SerializeField]
    private bool _canBeScaled = true;

    private float _RotateSpeed = 0.09f;

    private float _initialTouch0PosX;
    private float _currentTouch0PosX;

    private float _initialDistance;

    private Touch touch0;
    private Touch touch1;

    public delegate void OnObjectManipulation(float manipulationValue);
    public static event OnObjectManipulation onRotate;
    public static event OnObjectManipulation onScale;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            // Rotate
            if (Input.touchCount == 1 && _canBeScaled)
            {
                touch0 = Input.GetTouch(0);

                if (touch0.phase == TouchPhase.Began)
                {
                    _initialTouch0PosX = Input.GetTouch(0).position.x;
                }
                else if (touch0.phase == TouchPhase.Moved)
                {
                    _currentTouch0PosX = Input.GetTouch(0).position.x;

                    if (_currentTouch0PosX != _initialTouch0PosX)
                    {
                        float rotateValue = (_initialTouch0PosX - _currentTouch0PosX) * _RotateSpeed;

                        onRotate?.Invoke(rotateValue);
                    }

                    _initialTouch0PosX = touch0.position.x;
                }
            }
            // Scale
            else if (Input.touchCount == 2 && _canBeRotated)
            {
                touch0 = Input.GetTouch(0);
                touch1 = Input.GetTouch(1);

                if (touch0.phase == TouchPhase.Ended || touch0.phase == TouchPhase.Canceled
                   || touch1.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Canceled)
                {
                    return;
                }

                // It is enough to check whether one of them began since we
                // already excluded the Ended and Canceled phase in the line before
                if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
                {
                    _initialDistance = Vector2.Distance(touch0.position, touch1.position);
                }
                else
                {
                    var currentDistance = Vector2.Distance(touch0.position, touch1.position);

                    // A little emergency brake
                    if (Mathf.Approximately(_initialDistance, 0)) return;

                    // get the scale factor of the current distance relative to the inital one
                    var factor = currentDistance / _initialDistance;

                    // apply the scale
                    onScale?.Invoke(factor);
                }
            }

        }
    }
}
