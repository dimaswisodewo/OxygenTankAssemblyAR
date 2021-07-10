using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARObject : MonoBehaviour
{
    public ACTION_TYPE actionType;

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

    public void PauseAnimation()
    {
        animator.speed = 0f;
    }

    public void ResumeAnimation()
    {
        animator.speed = 1f;
    }
}
