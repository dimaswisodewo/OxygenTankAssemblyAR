using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public static class Tweening
{
    private static float _moveDuration = 1f;

    public static void MoveAnchorPosY(RectTransform rt, float to)
    {
        rt.DOAnchorPosY(to, _moveDuration);
    }
}
