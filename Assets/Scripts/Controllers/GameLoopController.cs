using UnityEngine;

public class GameLoopController : MonoBehaviour
{
    [SerializeField]
    private ConsoleInputHandler _consoleInput;
    [SerializeField]
    private FieldInitializer _fieldInitializer;
    [SerializeField]
    private UIContoller _uIContoller;

    private WinCriteria _winCriteria;

    /*    [SerializeField]
        private RobotMovement _robotMovement;*/

    private void Start()
    {
        InitGame();
        StartGame();
    }

    private void InitGame()
    {
        _fieldInitializer.InitFieldView();
    }

    public void StartTutorial()
    {
        _consoleInput.DisactivateConsole();
        //включить диалог робота
    }

    public void StartGame()
    {
        //отключить диалог робота
        _consoleInput.ActivateConsole();
    }

    public void FinishGame()
    {
        _consoleInput.DisactivateConsole();
        Debug.Log("Game Ended");
        //посчитать оценку
        string mark = "";
        mark+= _consoleInput.GetCountOfCommands().ToString();

        _uIContoller.SetupEndPanel(mark);
        //показать конечную панель
        _uIContoller.ShowEndPanel();
        //выдать спич робота в зависимости от нужны и оценки
    }
}


public class WinCriteria
{
    public WinCriteria(int five, int fore, int three)
    {
        countOfCommandForExelent = five;
        countOfCommandForGood = fore;
        countOfCommandForBad = three;
    }

    public int countOfCommandForExelent;
    public int countOfCommandForGood;
    public int countOfCommandForBad;
}