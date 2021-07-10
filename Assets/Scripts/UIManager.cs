using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject _uiCanvas;
    [SerializeField] private RectTransform _descriptionPanel;
    [SerializeField] private Text _descriptionText;
    [SerializeField] private Text _titleText;
    [SerializeField] private Text _togglePauseText;
    
    public Text TogglePauseText
    { 
        set { _togglePauseText = value; } 
        get { return _togglePauseText; }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        _uiCanvas.SetActive(false);
        _togglePauseText.text = Config.PAUSE_TEXT;
        _descriptionText.text = string.Empty;
    }

    public void InitializeUI(ACTION_TYPE actionType)
    {
        _togglePauseText.text = Config.PAUSE_TEXT;
        _titleText.text = (actionType == ACTION_TYPE.ASSEMBLY) ? Config.ASSEMBLY_TEXT : Config.DIASSEMBLY_TEXT;
    }

    public void SetActiveCanvasUI(bool setActive)
    {
        _uiCanvas.SetActive(setActive);
    }

    public void SetDescriptionText(string newText)
    {
        _descriptionText.text = newText;
    }

    public void ToggleDescriptionPanel()
    {
        if (_descriptionPanel.anchoredPosition.y == Config.DESCRIPTION_PANEL_BOT_POS)
        {
            SetDescriptionPanelPosition(Config.DESCRIPTION_PANEL_TOP_POS);
        }
        else
        {
            SetDescriptionPanelPosition(Config.DESCRIPTION_PANEL_BOT_POS);
        }
    }

    private void SetDescriptionPanelPosition(float to)
    {
        Tweening.MoveAnchorPosY(_descriptionPanel, to);
    }
}
