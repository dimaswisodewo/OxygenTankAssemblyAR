using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;

    [HideInInspector]
    public ARObject selectedARObject;

    public delegate void AnimationHandler();
    public event AnimationHandler onPlayAnimation;
    public event AnimationHandler onPauseAnimation;
    public event AnimationHandler onResumeAnimation;

    private int _currentDescIndex;

    private void Awake()
    {
        Application.targetFrameRate = 30;

        if (Instance == null)
            Instance = this;
    }

    private void Initialize()
    {
        onPlayAnimation += selectedARObject.RestartAnimation;
        onPauseAnimation += selectedARObject.PauseAnimation;
        onResumeAnimation += selectedARObject.ResumeAnimation;
    }

    private void Deinitialize()
    {
        onPlayAnimation = null;
        onPauseAnimation = null;
        onResumeAnimation = null;
    }

    public void OnTrackingFound()
    {
        Initialize();
        selectedARObject.gameObject.SetActive(true);

        UIManager.Instance.InitializeUI(selectedARObject.actionType);
        UIManager.Instance.SetActiveCanvasUI(true); UIManager.Instance.SetDescriptionText(JsonSerializer.Instance.GetDescriptionData(selectedARObject.actionType, 0));

        _currentDescIndex = 0;
    }

    public void OnTrackingLost()
    {
        selectedARObject?.gameObject.SetActive(false);
        UIManager.Instance.SetActiveCanvasUI(false);

        Deinitialize();
    }

    public void OnPlayButtonClick()
    {
        onPlayAnimation?.Invoke();
    }

    public void OnTogglePauseButtonClick()
    {
        if (UIManager.Instance.TogglePauseText.text == Config.PAUSE_TEXT)
        {
            onPauseAnimation?.Invoke();
            UIManager.Instance.TogglePauseText.text = Config.RESUME_TEXT;
        }
        else
        {
            onResumeAnimation?.Invoke();
            UIManager.Instance.TogglePauseText.text = Config.PAUSE_TEXT;
        }
    }

    public void OnToggleDescriptionButtonClick()
    {
        UIManager.Instance.ToggleDescriptionPanel();
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
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
        UIManager.Instance.SetDescriptionText(JsonSerializer.Instance.GetDescriptionData(selectedARObject.actionType, _currentDescIndex));
    }

    public void OnPrevDescButtonClick()
    {
        if (_currentDescIndex - 1 < 0)
        {
            Debug.Log("Reached the first item");
            return;
        }

        _currentDescIndex -= 1;
        UIManager.Instance.SetDescriptionText(JsonSerializer.Instance.GetDescriptionData(selectedARObject.actionType, _currentDescIndex));
    }
}

public enum ACTION_TYPE
{
    ASSEMBLY,
    DIASSEMBLY
}
