using UnityEngine;

public sealed class EndingProgrammListener : MonoBehaviour
{
    [SerializeField]
    private GameLoopController _gameLoopController;
    [SerializeField]
    private CommandParser _commandParser;
    [SerializeField]
    private ConsoleInputHandler _consoleInput;
    [SerializeField]
    private RobotMovement _robotMovement;

    [SerializeField]
    private InputValuesMatcher _inputValuesMatcher;
    [SerializeField]
    private bool _isNeedToMatchVariables;
    //private 

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
        //���� ����� � ������ �����
        if (_robotMovement.Coordinates == _fieldInitializer.WinCoordinates)
        {
            Debug.Log("����� � �������� �����������");
            //���� ��������� �������������� ������� (��� ������� ������ ����)
            if (CheckAdditionalConditions())
            {
                _gameLoopController.FinishGame();
            }
            else
            {
                _consoleInput.ActivateConsole();
                Debug.Log("��� ������� ������ �� ���������");
            }
        }
        else
        {
            _consoleInput.ActivateConsole();
            Debug.Log("����� �� � ��� �����");
            //�������� ����� ��������� ���� ����� ������ � ����� �������
        }
    }

    private bool CheckAdditionalConditions()
    {
        bool isLevelPassed= !_commandParser.IsProgramCrashed;

        if (_inputValuesMatcher != null && _isNeedToMatchVariables)
        {
            isLevelPassed = isLevelPassed && _inputValuesMatcher.IsAnswerCorrect;
        }

        return isLevelPassed;
    }
}