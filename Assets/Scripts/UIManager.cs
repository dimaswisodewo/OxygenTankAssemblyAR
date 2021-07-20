using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject _uiCanvas;
    [SerializeField] private GameObject _instructionCanvas;
    [SerializeField] private GameObject _warningPanel;
    [SerializeField] private RectTransform _descriptionPanel;
    [SerializeField] private RectTransform _sideButtons;
    [SerializeField] private Text _descriptionText;
    [SerializeField] private Text _warningText;
    [SerializeField] private Text _titleText;
    [SerializeField] private Text _togglePauseText;
    [SerializeField] private Text _toggleShowHideText;

    private bool _isDescriptionPanelActive = false;
    private bool _isSideButtonsShowing = true;
    
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

        if (ARContentManager.Instance.arContent == AR_CONTENT.ASSEMBLY)
        {
            _titleText.text = Config.ASSEMBLY_TEXT;
        }
        else
        {
            _titleText.text = Config.DIASSEMBLY_TEXT;
        }
    }

    public void InitializeUI()
    {
        _togglePauseText.text = Config.PAUSE_TEXT;
        //_titleText.text = (actionType == ACTION_TYPE.ASSEMBLY) ? Config.ASSEMBLY_TEXT : Config.DIASSEMBLY_TEXT;
    }

    public void SetActiveCanvasUI(bool setActive)
    {
        _uiCanvas.SetActive(setActive);
    }

    public void SetActiveInstructionCanvas(bool setActive)
    {
        _instructionCanvas.SetActive(setActive);
    }

    private void SetDescriptionText(string newText)
    {
        _descriptionText.text = newText;
    }

    private void SetWarningText(string newText)
    {
        _warningText.text = newText;
    }

    public void SetTextContent(ACTION_TYPE actionType, int index)
    {
        SetDescriptionText(JsonSerializer.Instance.GetDescriptionData(actionType, index));

        if (string.IsNullOrEmpty(JsonSerializer.Instance.GetWarningData(actionType, index)))
        {
            SetWarningText(string.Empty);
            _warningPanel.SetActive(false);
            Debug.Log("SetActive False " + _warningPanel.activeInHierarchy);
        }
        else
        {
            SetWarningText(JsonSerializer.Instance.GetWarningData(actionType, index));
            _warningPanel.SetActive(true);
            Debug.Log("SetActive True " + _warningPanel.activeInHierarchy);
        }
    }

    public void ToggleDescriptionPanel()
    {
        if (_isDescriptionPanelActive)
        {
            SetDescriptionPanelPosition(Config.DESCRIPTION_PANEL_BOT_POS);
            _isDescriptionPanelActive = false;
        }
        else
        {
            SetDescriptionPanelPosition(Config.DESCRIPTION_PANEL_TOP_POS);
            _isDescriptionPanelActive = true;
        }
    }

    public void ToggleSideButtons()
    {
        if (_isSideButtonsShowing)
        {
            Tweening.MoveAnchorPosX(_sideButtons, Config.SIDE_BUTTONS_HIDDEN_POS);
            _toggleShowHideText.text = Config.SHOW_TEXT;
            _isSideButtonsShowing = false;
        }
        else
        {
            Tweening.MoveAnchorPosX(_sideButtons, Config.SIDE_BUTTONS_SHOWN_POS);
            _toggleShowHideText.text = Config.HIDE_TEXT;
            _isSideButtonsShowing = true;
        }
    }

    private void SetDescriptionPanelPosition(float to)
    {
        Tweening.MoveAnchorPosY(_descriptionPanel, to);
    }
}
