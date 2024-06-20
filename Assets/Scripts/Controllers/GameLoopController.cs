using UnityEngine;
using System.Collections.Generic;

public sealed class GameLoopController : MonoBehaviour
{
    [SerializeField]
    private ConsoleInputHandler _consoleInput;
    [SerializeField]
    private FieldInitializer _fieldInitializer;
    [SerializeField]
    private UIContoller _uIContoller;
    [SerializeField]
    private TutorialController _tutorialController;

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
        //включить диалог робота
        _tutorialController.OpenDialogueFromStart();
        StartGame();
    }

    public void StartGame()
    {
        //отключить диалог робота
        _consoleInput.ActivateConsole();
    }

    public void FinishGame()
    {
        Debug.Log("Game Ended");

        

        _consoleInput.DisactivateConsole();
        //посчитать и вывести оценку за игру
        var countOfCommands = _consoleInput.GetCountOfCommands();
        int indexOfMark = GetMarkIndex(countOfCommands);
        string mark = "ќценка - "+(indexOfMark +3).ToString()+ "!";
        string markConclusion;
        if (indexOfMark == -1)
        {
            markConclusion = _textForLose;
            _uIContoller.HideNextButtonOnEndPanel();

            //назначить роботу текст плохой концовки
            _tutorialController.SetTextToBadEnd();
        }
        else
        {
            markConclusion = _textsForMarks[indexOfMark];
            _uIContoller.ShowNextButtonOnEndPanel();

            //назначить роботу текст хорошей концовки
            _tutorialController.SetTextToGoodEnd();
        }

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