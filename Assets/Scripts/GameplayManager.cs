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

    private int _currentDescIndex;

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
        selectedARObject.gameObject.SetActive(true);

        UIManager.Instance.InitializeUI();
        UIManager.Instance.SetActiveCanvasUI(true);

        _currentDescIndex = 0;
        UIManager.Instance.SetTextContent(selectedARObject.actionType, _currentDescIndex);

        selectedARObject.PauseAnimation();
        selectedARObject.PlayAnimationOnStep(_currentDescIndex);
    }

    public void OnTrackingLost()
    {
        selectedARObject?.gameObject.SetActive(false);
        UIManager.Instance.SetActiveCanvasUI(false);

        //Deinitialize();
    }

    public void OnPlayButtonClick()
    {
        selectedARObject?.PlayAnimationOnStep(_currentDescIndex);
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            OnNextDescButtonClick();
    }

    public void OnNextDescButtonClick()
    {
        int dataCount = (selectedARObject.actionType == ACTION_TYPE.ASSEMBLY ? JsonSerializer.Instance.GetAssembleDataCount() : JsonSerializer.Instance.GetDiassembleDataCount());

        if (_currentDescIndex + 1 >= dataCount)
        {
            Debug.Log("Reached the last item");
            return;
        }

        _currentDescIndex += 1;
        UIManager.Instance.SetTextContent(selectedARObject.actionType, _currentDescIndex);

        selectedARObject.PlayAnimationOnStep(_currentDescIndex);
    }

    public void OnPrevDescButtonClick()
    {
        if (_currentDescIndex - 1 < 0)
        {
            Debug.Log("Reached the first item");
            return;
        }

        _currentDescIndex -= 1;
        UIManager.Instance.SetTextContent(selectedARObject.actionType, _currentDescIndex);

        selectedARObject.PlayAnimationOnStep(_currentDescIndex);
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
