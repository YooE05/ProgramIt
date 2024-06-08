using UnityEngine;

public class EndProgrammListener : MonoBehaviour
{
    [SerializeField]
    private GameLoopController _gameLoopController;
    [SerializeField]
    private CommandParser _commandParser;
    [SerializeField]
    private ConsoleInputHandler _consoleInput;
    [SerializeField]
    private RobotMovement _robotMovement;

    [Header("Conditions")]
    [SerializeField] FieldInitializer _fieldInitializer;

    private void OnEnable()
    {
        _commandParser.OnProgramStopped += CheckWinConditions;
    }

    private void OnDisable()
    {
        _commandParser.OnProgramStopped -= CheckWinConditions;
    }

    private void CheckWinConditions()
    {
        //если робот в нужной точке
        if (_robotMovement.Coordinates == _fieldInitializer.WinCoordinates)
        {
            Debug.Log("Робот в победных координатах");
            //если соблюдены дополнительные условия (для каждого уровня свои)
            if (AdditionalConditionsMet())
            {
                _gameLoopController.FinishGame();
            }
            else
            {
                _consoleInput.ActivateConsole();
                Debug.Log("Доп условия победы не соблюдены");
            }
        }
        else
        {
            _consoleInput.ActivateConsole();
            Debug.Log("Робот не в той точке");
            //возможно стоит перенести сюда вывод ошибки в дебаг консоли
        }
    }

    private bool AdditionalConditionsMet()
    {
        return !_commandParser.IsProgramCrashed;
    }
}