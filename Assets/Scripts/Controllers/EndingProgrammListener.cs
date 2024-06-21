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
                _debugString.text = "����� � ������ �����, �� �������������� ������� �� ���������";
             //   Debug.Log("��� ������� ������ �� ���������");
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
