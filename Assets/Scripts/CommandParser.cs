using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public sealed class CommandParser : MonoBehaviour
{
    public delegate void CommandAction();
    public CommandAction _currentAction;

    [SerializeField]
    private RobotMovement _robotMovement;
    // private bool _robotMovementIsActive;

    private List<Command> _currentCommandLine = new();
    private int _stepsCount = -1;

    [SerializeField]
    private TextMeshProUGUI _debugString;
    private string _debugStringValue;
    private bool _needToStopProgram;

    public IEnumerator ReadProgram(List<Command> commands, List<Command> additionalCommands = null)
    {
        InitConsole();
        _robotMovement.InitRobot();

        if (commands.Count == 0)
        {
            _debugStringValue = "no code";
        }
        else
        {
            //��� ������ ������, ����� ������� ������ ����������� ��� ���� ��������� ������
            for (int i = 0; i < commands.Count; i++)
            {
                if (_needToStopProgram)
                {
                    break;
                }

                _currentAction = DefineAction(commands[i]);

                if (i == 0 && _currentAction != Activate)
                {
                    _debugStringValue = "need to activate robot first";
                    break;
                }
                else if (_robotMovement.IsActive && i == commands.Count - 1 && additionalCommands == null && _currentAction != Disactivate || commands.Count < 2)
                {
                    _debugStringValue = "need to disactivate robot at the end";
                    break;
                }
                else if (i != 0 && !_robotMovement.IsActive)
                {
                    _debugStringValue = "robot can't act after first disactivation";
                    break;
                }
                else
                {
                    //  RealiseAction(_currentAction, commands[i]);
                    yield return RealiseAction(_currentAction, commands[i]);
                }
            }
        }

        //������� ������ ����� ����� ���������

        if (_debugStringValue == string.Empty)
        {
            _debugStringValue = "it's fine";
        }

        Debug.Log(_debugStringValue);

        _debugString.text = _debugStringValue;

        yield return null;
    }

    private IEnumerator RealiseAction(CommandAction currentAction, Command currendCommand)
    {
        if (currendCommand.IsNumber() && _currentCommandLine.Count >= 2 && _stepsCount >= 0)
        {
            for (int i = 0; i < _stepsCount; i++)
            {
                if (_needToStopProgram)
                {
                    break;
                }
                currentAction();
                yield return new WaitForSeconds(1f);
            }
            _stepsCount = -1;
            _currentCommandLine.Clear();
        }
        else
        {
            currentAction();
            yield return new WaitForSeconds(1f);
        }
    }

    private void InitConsole()
    {
        _needToStopProgram = false;
        _debugStringValue = string.Empty;
        _currentCommandLine.Clear();
        _stepsCount = -1;
        _robotMovement.IsActive = false;
    }

    private CommandAction DefineAction(Command command)
    {
        CommandAction returnedAction = () => { Debug.Log("incorrect command"); };

        if (_currentCommandLine.Count > 0)
        {
            if (_currentCommandLine[0].ActionValue == ButtonInputValues.Go && command.IsNumber())
            {
                returnedAction = Move;
                _currentCommandLine.Add(command);
                switch (command.ActionValue)
                {
                    case ButtonInputValues.Zero:
                        _stepsCount = 0;
                        break;
                    case ButtonInputValues.One:
                        _stepsCount = 1;
                        break;
                    case ButtonInputValues.Two:
                        _stepsCount = 2;
                        break;
                    case ButtonInputValues.Three:
                        _stepsCount = 3;
                        break;
                    case ButtonInputValues.For:
                        _stepsCount = 4;
                        break;
                    case ButtonInputValues.Five:
                        _stepsCount = 5;
                        break;
                    case ButtonInputValues.Six:
                        _stepsCount = 6;
                        break;
                    case ButtonInputValues.Seven:
                        _stepsCount = 7;
                        break;
                    case ButtonInputValues.Eight:
                        _stepsCount = 8;
                        break;
                    case ButtonInputValues.Nine:
                        _stepsCount = 9;
                        break;
                    default:
                        _stepsCount = 0;
                        break;
                }
            }
        }
        else
        {
            switch (command.ActionValue)
            {
                case ButtonInputValues.Zero:
                    returnedAction = StopProgramm;
                    break;
                case ButtonInputValues.One:
                    returnedAction = StopProgramm;
                    break;
                case ButtonInputValues.Two:
                    returnedAction = StopProgramm;
                    break;
                case ButtonInputValues.Three:
                    returnedAction = StopProgramm;
                    break;
                case ButtonInputValues.For:
                    returnedAction = StopProgramm;
                    break;
                case ButtonInputValues.Five:
                    returnedAction = StopProgramm;
                    break;
                case ButtonInputValues.Six:
                    returnedAction = StopProgramm;
                    break;
                case ButtonInputValues.Seven:
                    returnedAction = StopProgramm;
                    break;
                case ButtonInputValues.Eight:
                    returnedAction = StopProgramm;
                    break;
                case ButtonInputValues.Nine:
                    returnedAction = StopProgramm;
                    break;
                case ButtonInputValues.Activate:
                    returnedAction = Activate;
                    break;
                case ButtonInputValues.Disactivate:
                    returnedAction = Disactivate;
                    break;
                case ButtonInputValues.Right:
                    returnedAction = RotateRight;
                    break;
                case ButtonInputValues.Left:
                    returnedAction = RotateLeft;
                    break;
                case ButtonInputValues.Go:
                    _currentCommandLine.Add(command);
                    returnedAction = () => { Debug.Log("Waiting for number of steps"); };
                    break;
                default:
                    returnedAction = () => { Debug.Log("incorrect command"); };
                    break;
            }
        }

        return returnedAction;
    }

    private void StopProgramm()
    {
        _needToStopProgram = true;
        _debugStringValue = "programm was stopped - incorrect line";
        Debug.Log("programm was stopped - incorrect line");
    }

    private void Activate()
    {
        if (_robotMovement.IsActive)
        {
            _debugStringValue = "ERROR, you already activated robot!";
            Debug.Log("ERROR, you already activated robot!");
        }
        else
        {
            Debug.Log("Activated");
            _robotMovement.ChangeActivationStage(true);
        }
    }

    private void Disactivate()
    {
        if (_robotMovement.IsActive)
        {
            Debug.Log("Disactivated");
            _robotMovement.ChangeActivationStage(false);
        }
        else
        {
            _debugStringValue = "You need to activate robot first";
            Debug.Log("You need to activate robot first");
        }
    }

    private void RotateLeft()
    {
        _robotMovement.RotateLeft();
        Debug.Log("Rotate left");
    }

    private void RotateRight()
    {
        _robotMovement.RotateRight();
        Debug.Log("Rotate right");
    }

    private void Move()
    {
        if (_robotMovement.TryMoveForward())
        {
            Debug.Log("Move");
        }
        else
        {
            StopProgramm();
            //������� ����� �� ������ �������� ������ ���� �����
            _debugStringValue = "Robot can't move to next cell";
        }

        _currentCommandLine.Clear();
    }
}
