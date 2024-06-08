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
        //���� ����� � ������ �����
        if (_robotMovement.Coordinates == _fieldInitializer.WinCoordinates)
        {
            Debug.Log("����� � �������� �����������");
            //���� ��������� �������������� ������� (��� ������� ������ ����)
            if (AdditionalConditionsMet())
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

    private bool AdditionalConditionsMet()
    {
        return !_commandParser.IsProgramCrashed;
    }
}