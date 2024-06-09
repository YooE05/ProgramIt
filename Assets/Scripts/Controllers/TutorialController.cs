using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public sealed class TutorialController : MonoBehaviour
{
    public Action OnDialogueOpened;
    public Action OnDialogueClosed;

    [Header("Robot View")]
    [SerializeField]
    private Image _robotImage;
    [SerializeField]
    private Button _robotButton;
    [SerializeField]
    private Sprite _closedDialogueSprite;
    [SerializeField]
    private Sprite _openDialogueSprite;

    [Header("Panels")]
    [SerializeField]
    private GameObject _textPanel;
    [SerializeField]
    private TextMeshProUGUI _tutorialText;

    [SerializeField]
    private GameObject _previousButton;
    [SerializeField]
    private GameObject _nextButton;

    [Header("Tutorial texts")]
    [SerializeField]
    private List<string> _startTutorialText = new();
    [SerializeField]
    private List<string> _goodEndTutorialText = new();
    [SerializeField]
    private List<string> _badEndTutorialText = new();

    private List<string> _currentTutorialText = new();
    private int _currntTutorialIndex;

    private void Awake()
    {
        _previousButton.GetComponent<Button>().onClick.AddListener(ShowPreviousMessage);
        _nextButton.GetComponent<Button>().onClick.AddListener(ShowNextMessage);
    }

    public void SetTextToStart()
    {
        _currentTutorialText = _startTutorialText;
    }

    public void SetTextToGoodEnd()
    {
        _currentTutorialText = _goodEndTutorialText;
    }

    public void SetTextToBadEnd()
    {
        _currentTutorialText = _badEndTutorialText;
    }

    public void CloseDialogue()
    {
        //закрыть текстовую панель
        _textPanel.SetActive(false);
        //сменить спрайт у робота
        _robotImage.sprite = _closedDialogueSprite;

        //переназначить кнопку робота на открытие
        _robotButton.onClick.RemoveAllListeners();
        _robotButton.onClick.AddListener(OpenDialogueFromStart);
    }

    public void OpenDialogueFromStart()
    {
        //открыть текстовую панель под нулевым текстом
        _currntTutorialIndex = 0;
        SetUpTextPanel();
        _textPanel.SetActive(true);

        //сменить спрайт у робота
        _robotImage.sprite = _openDialogueSprite;

        //переназначить кнопку робота на закрытие
        _robotButton.onClick.RemoveAllListeners();
        _robotButton.onClick.AddListener(CloseDialogue);
    }

    private void SetUpTextPanel()
    {
        if (_currentTutorialText.Count > 0)
        {
            _tutorialText.text = _currentTutorialText[_currntTutorialIndex];
        }
        else
        {
            _tutorialText.text = "Мне как-то даже нечего тебе сказать..";
        }

        _previousButton.SetActive(true);
        _nextButton.SetActive(true);
        if (_currntTutorialIndex == 0)
        {
            _previousButton.SetActive(false);
        }
        if (_currntTutorialIndex == _currentTutorialText.Count - 1)
        {
            _nextButton.SetActive(false);
        }
    }

    private void ShowPreviousMessage()
    {
        _currntTutorialIndex--;
        if (_currntTutorialIndex >= 0)
        {
            SetUpTextPanel();
        }
    }

    private void ShowNextMessage()
    {
        _currntTutorialIndex++;
        if (_currntTutorialIndex < _currentTutorialText.Count)
        {
            SetUpTextPanel();
        }
    }
}
