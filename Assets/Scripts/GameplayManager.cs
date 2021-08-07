using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;

    public int activeMarkerCount = 0;

    [HideInInspector]
    public ARObject selectedARObject;

    [HideInInspector]
    public bool _isRotated;

    //public delegate void AnimationHandler();
    //public event AnimationHandler onPauseAnimation;
    //public event AnimationHandler onResumeAnimation;

    public Coroutine animationCoroutine;

    [HideInInspector]
    public int currentDescIndex;

    private int dataCount = -1;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    //private void Initialize()
    //{
    //    onPauseAnimation += selectedARObject.PauseAnimation;
    //    onResumeAnimation += selectedARObject.ResumeAnimation;
    //}

    //private void Deinitialize()
    //{
    //    onPauseAnimation = null;
    //    onResumeAnimation = null;
    //}

    public void OnTrackingFound()
    {
        //Initialize();
        if (dataCount == -1)
        {
            dataCount = (selectedARObject.actionType == ACTION_TYPE.ASSEMBLY ? JsonSerializer.Instance.GetAssembleDataCount() : JsonSerializer.Instance.GetDiassembleDataCount());
        }

        selectedARObject.gameObject.SetActive(true);

        UIManager.Instance.InitializeUI();
        UIManager.Instance.SetActiveCanvasUI(true);

        currentDescIndex = 0;
        UIManager.Instance.SetTextContent(selectedARObject.actionType, currentDescIndex);
        UIManager.Instance.previousButton.interactable = false;

        selectedARObject.PauseAnimation();
        selectedARObject.PlayAnimationOnStep(currentDescIndex);
    }

    public void OnTrackingLost()
    {
        selectedARObject?.gameObject.SetActive(false);
        UIManager.Instance.SetActiveCanvasUI(false);

        //Deinitialize();
    }

    public void OnPlayButtonClick()
    {
        selectedARObject?.PlayAnimationOnStep(currentDescIndex);
    }

    //public void OnTogglePauseButtonClick()
    //{
    //    if (UIManager.Instance.TogglePauseText.text == Config.PAUSE_TEXT)
    //    {
    //        onPauseAnimation?.Invoke();
    //        UIManager.Instance.TogglePauseText.text = Config.RESUME_TEXT;
    //    }
    //    else
    //    {
    //        onResumeAnimation?.Invoke();
    //        UIManager.Instance.TogglePauseText.text = Config.PAUSE_TEXT;
    //    }
    //}

    public void OnToggleDescriptionButtonClick()
    {
        UIManager.Instance.ToggleDescriptionPanel();
    }

    public void OnNextDescButtonClick()
    {
        if (currentDescIndex + 1 >= dataCount)
        {
            Debug.Log("Reached the first item");
            return;
        }

        currentDescIndex += 1;
        UIManager.Instance.SetTextContent(selectedARObject.actionType, currentDescIndex);
        UIManager.Instance.PositionCheckingDescriptionPanel();

        selectedARObject.PlayAnimationOnStep(currentDescIndex);

        if (currentDescIndex + 1 >= dataCount)
        {
            UIManager.Instance.nextButton.interactable = false;
        }
        else
        {
            UIManager.Instance.previousButton.interactable = true;
            UIManager.Instance.nextButton.interactable = true;
        }
    }

    public void OnPrevDescButtonClick()
    {
        if (currentDescIndex - 1 < 0)
        {
            Debug.Log("Reached the last item");
            return;
        }

        currentDescIndex -= 1;
        UIManager.Instance.SetTextContent(selectedARObject.actionType, currentDescIndex);
        UIManager.Instance.PositionCheckingDescriptionPanel();

        selectedARObject.PlayAnimationOnStep(currentDescIndex);

        if (currentDescIndex - 1 < 0)
        {
            UIManager.Instance.previousButton.interactable = false;
        }
        else
        {
            UIManager.Instance.previousButton.interactable = true;
            UIManager.Instance.nextButton.interactable = true;
        }
    }

    public void OnRotate45ButtonClick()
    {
        if (_isRotated)
        {
            selectedARObject.RotateParentX(-45f);
            _isRotated = false;
        }
        else
        {
            selectedARObject.RotateParentX(45f);
            _isRotated = true;
        }
    }
}

public enum ACTION_TYPE
{
    ASSEMBLY,
    DIASSEMBLY
}
