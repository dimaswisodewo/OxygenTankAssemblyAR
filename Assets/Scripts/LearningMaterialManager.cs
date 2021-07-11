using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LearningMaterialManager : MonoBehaviour
{
    [SerializeField]
    private Text _titleText;

    [SerializeField]
    private LearningMaterialData _safety;

    [SerializeField]
    private LearningMaterialData _oxygen;

    private RectTransform _displayedContent;
    private int _displayedContentCount;
    private int _currentIndex = 0;
    private const int _moveValue = 800;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (ARContentManager.Instance.learningContent == LEARNING_CONTENT.SAFETY)
        {
            _titleText.text = Config.SAFETY_TEXT;
            _displayedContent = _safety.GetComponent<RectTransform>();
            _displayedContentCount = _safety.panelCount;

            _safety.gameObject.SetActive(true);
            _oxygen.gameObject.SetActive(false);
        }
        else
        {
            _titleText.text = Config.OXYGEN_TEXT;
            _displayedContent = _oxygen.GetComponent<RectTransform>();
            _displayedContentCount = _oxygen.panelCount;

            _safety.gameObject.SetActive(false);
            _oxygen.gameObject.SetActive(true);
        }
    }

    public void OnPreviousButtonClick()
    {
        if (_currentIndex - 1 < 0) return;

        _currentIndex -= 1;
        MoveLearningPanel();
    }

    public void OnNextButtonClick()
    {
        if (_currentIndex + 1 > _displayedContentCount - 1) return;

        _currentIndex += 1;
        MoveLearningPanel();
    }

    private void MoveLearningPanel()
    {
        Tweening.MoveAnchorPosX(_displayedContent, _currentIndex * -_moveValue);
    }
}

