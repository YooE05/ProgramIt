using UnityEngine;
using System.Collections.Generic;

public sealed class GameLoopController : MonoBehaviour
{
    [SerializeField]
    private int _levelIndex;

    [SerializeField]
    private ConsoleInputHandler _consoleInput;
    [SerializeField]
    private FieldInitializer _fieldInitializer;
    [SerializeField]
    private UIContoller _uIContoller;
    [SerializeField]
    private TutorialController _tutorialController;

    [SerializeField]
    private GameObject _playerGameObject;


    [Header("Mark criteria")]
    [SerializeField]
    private List<int> _criteriaList = new();
    [SerializeField]
    private List<string> _textsForMarks = new();
    [SerializeField]
    private string _textForLose;

    private void Start()
    {
        InitGame();
        StartTutorial();
    }

    private void InitGame()
    {
        _fieldInitializer.InitFieldView();
        _tutorialController.SetTextToStart();
    }

    public void StartTutorial()
    {
        //�������� ������ ������
        _tutorialController.OpenDialogueFromStart();
        StartGame();
    }

    public void StartGame()
    {
        //��������� ������ ������
        _consoleInput.ActivateConsole();
    }

    public void FinishGame()
    {
        Debug.Log("Game Ended");

        _fieldInitializer.HideField();
        _playerGameObject.SetActive(false);

        _consoleInput.DisactivateConsole();
        //��������� � ������� ������ �� ����
        var countOfCommands = _consoleInput.GetCountOfCommands();
        int indexOfMark = GetMarkIndex(countOfCommands);
        string mark = "������ - "+(indexOfMark +3).ToString()+ "!";
        string markConclusion;
        if (indexOfMark == -1)
        {
            markConclusion = _textForLose;
            _uIContoller.HideNextButtonOnEndPanel();

            //��������� ������ ����� ������ ��������
            _tutorialController.SetTextToBadEnd();
        }
        else
        {
            markConclusion = _textsForMarks[indexOfMark];
            _uIContoller.ShowNextButtonOnEndPanel();

            //��������� ������ ����� ������� ��������
            _tutorialController.SetTextToGoodEnd();
        }

        DataController.Instance.SetMark(indexOfMark + 3, _levelIndex);
        DataController.Instance.SaveData();

        _uIContoller.SetupEndPanel(mark, markConclusion);
        _uIContoller.ShowEndPanel();

        _tutorialController.OpenDialogueFromStart();
    }

    private int GetMarkIndex(int countOfCommands)
    {
        int index = -1;

        if (countOfCommands < _criteriaList[0])
        {
            index = 0;
        }
        if (countOfCommands < _criteriaList[1])
        {
            index = 1;
        }
        if (countOfCommands < _criteriaList[2])
        {
            index = 2;
        }

        return index;
    }
}