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
    
    private float[][] _animationStep;
    private float _animationFrameCount;

    private void Awake()
    {
        gameObject.SetActive(false);

        if (actionType == ACTION_TYPE.ASSEMBLY)
        {
            _animationStep = Config.ASSEMBLY_ANIMATION_STEP;
            _animationFrameCount = Config.ASSEMBLY_FRAME_COUNT;
        }
        else
        {
            _animationStep = Config.DIASSEMBLY_ANIMATION_STEP;
            _animationFrameCount = Config.DIASSEMBLY_FRAME_COUNT;
        }
    }

    public void RestartAnimation()
    {
        if (!animator.isActiveAndEnabled) animator.enabled = true;
        animator.Play(ANIMATION_STATE_ACTION, -1, 0f); // Reset animation state to first layer & start at 0 time
    }

    public void PlayAnimationOnStep(int stepIndex)
    {
        float frameFrom = _animationStep[stepIndex][0];
        float frameTo = _animationStep[stepIndex][1];

        if (frameTo > _animationFrameCount) frameTo = _animationFrameCount;

        Debug.Log($"Play animation step {stepIndex}, from frame {frameFrom} to frame {frameTo}");
        float startTime = frameFrom / 30f;
        float endTime = frameTo / 30f;
        float timeCount = _animationFrameCount / 30f;

        Debug.Log("Play anim");

        animator.Play(ANIMATION_STATE_ACTION, -1, startTime / timeCount); // startTime value is normalized
        ResumeAnimation();

        if (GameplayManager.Instance.animationCoroutine != null)
            StopCoroutine(GameplayManager.Instance.animationCoroutine);

        GameplayManager.Instance.animationCoroutine = StartCoroutine(PlayAnimCoroutine());
        IEnumerator PlayAnimCoroutine()
        {
            UIManager.Instance.replayButton.interactable = false;
            yield return new WaitForSeconds(endTime - startTime);

            Debug.Log("Stop anim");
            PauseAnimation();
            UIManager.Instance.replayButton.interactable = true;
        }
    }

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
