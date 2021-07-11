using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject _uiCanvas;
    [SerializeField] private GameObject _instructionCanvas;
    [SerializeField] private RectTransform _descriptionPanel;
    [SerializeField] private Text _descriptionText;
    [SerializeField] private Text _titleText;
    [SerializeField] private Text _togglePauseText;

    private bool _isDescriptionPanelActive = false;
    
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

    public void SetDescriptionText(string newText)
    {
        _descriptionText.text = newText;
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

    private void SetDescriptionPanelPosition(float to)
    {
        Tweening.MoveAnchorPosY(_descriptionPanel, to);
    }
}
