using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public static class Tweening
{
    private static float _moveYDuration = 1f;
    private static float _moveXDuration = 0.3f;

    public static void MoveAnchorPosY(RectTransform rt, float to)
    {
        rt.DOAnchorPosY(to, _moveYDuration);
    }

    public static void MoveAnchorPosX(RectTransform rt, float to)
    {
        rt.DOAnchorPosX(to, _moveXDuration);
    }
}
