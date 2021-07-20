using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARObject : MonoBehaviour
{
    public ACTION_TYPE actionType;

    [SerializeField]
    private Transform parent;

    [SerializeField]
    private Animator animator;

    private const string ANIMATION_STATE_ACTION = "Action";

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void RestartAnimation()
    {
        if (!animator.isActiveAndEnabled) animator.enabled = true;
        animator.Play(ANIMATION_STATE_ACTION, -1, 0f); // Reset animation state to first layer & start at 0 time
    }

    //public void PlayAnimationOnFrame(int frameFrom, int frameTo)
    //{
    //    float startTime = frameFrom / 30f;
    //    float endTime = frameTo / 30f;
    //    animator.Play(ANIMATION_STATE_ACTION, -1, startTime);
    //}

    public void PauseAnimation()
    {
        animator.speed = 0f;
    }

    public void ResumeAnimation()
    {
        animator.speed = 1f;
    }

    public void ResetObject()
    {
        transform.localEulerAngles = Vector3.zero;
        transform.localScale = Vector3.one;
        parent.localEulerAngles = Vector3.zero;
        GameplayManager.Instance._isRotated = false;
    }

    public void RotateParentX(float angle)
    {
        parent.Rotate(Vector3.right, angle);
    }
}
