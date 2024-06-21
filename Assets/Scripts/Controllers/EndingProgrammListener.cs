using TMPro;
using UnityEngine;

public sealed class EndingProgrammListener : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _debugString;

    [SerializeField]
    private GameLoopController _gameLoopController;
    [SerializeField]
    private CommandParser _commandParser;
    [SerializeField]
    private ConsoleInputHandler _consoleInput;
    [SerializeField]
    private RobotMovement _robotMovement;


    [Header("Conditions")]
    [SerializeField]
    private InputValuesMatcher _inputValuesMatcher;
    [SerializeField]
    private LeversController _leversController;

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
            if (CheckAdditionalConditions())
            {
                _gameLoopController.FinishGame();
            }
            else
            {
                _consoleInput.ActivateConsole();
                _debugString.text = "Робот в нужной точке, но дополнительные условия не соблюдены";
             //   Debug.Log("Доп условия победы не соблюдены");
            }
        }
        else
        {
            _consoleInput.ActivateConsole();
            Debug.Log("Робот не в той точке");
            //возможно стоит перенести сюда вывод ошибки в дебаг консоли
        }
    }

    private bool CheckAdditionalConditions()
    {
        bool isLevelPassed = !_commandParser.IsProgramCrashed;

        if (_inputValuesMatcher != null)
        {
            if (_inputValuesMatcher.IsActiveCondition)
            {
                isLevelPassed = isLevelPassed && _inputValuesMatcher.IsAnswerCorrect;
            }
        }

        if (_leversController != null)
        {
            isLevelPassed = isLevelPassed && _leversController.CheckAllLeverPulls();
        }

        return isLevelPassed;
    }
}
