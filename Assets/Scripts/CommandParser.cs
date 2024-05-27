using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class CommandParser : MonoBehaviour
{
    private Vector2 _coordinates;
    private bool _isActive;

    private List<Command> _currentCommandLine = new();
    private int _stepsCount = -1;
    private string _debugString;

    public delegate void CommandAction();
    public CommandAction _currentAction;


    private void InitRobot(Vector2 startCoordinates)
    {
        _coordinates = startCoordinates;
    }

    internal string ReadProgram(List<Command> commands, List<Command> additionalCommands = null)
    {
        InitConsole();

        if (commands.Count == 0)
        {
            return "no code";
        }
        else
        {
            //для начала сделаю, чтобы команды просто считывались без учёт положения робота
            for (int i = 0; i < commands.Count; i++)
            {
                _currentAction = DefineAction(commands[i]);

                if (i == 0 && _currentAction != Activate)
                {
                    return "need to activate robot first";
                }
                else if (_isActive && i == commands.Count - 1 && additionalCommands == null && _currentAction != Disactivate || commands.Count < 2)
                {
                    return "need to disactivate robot at the end";
                }
                else if (i != 0 && !_isActive)
                {
                    return "robot can't act after first disactivation";
                }
                else
                {
                    RealiseAction(_currentAction, commands[i]);
                }
            }
        }

        //Сделать выводы после конца программы

        if (_debugString == string.Empty)
        {
            _debugString = "it's fine";
        }

        return _debugString;
    }

    private void RealiseAction(CommandAction currentAction, Command currendCommand)
    {
        if (currendCommand.IsNumber() && _currentCommandLine.Count >= 2 && _stepsCount >= 0)
        {
            for (int i = 0; i < _stepsCount; i++)
            {
                currentAction();
            }
            _stepsCount = -1;
            _currentCommandLine.Clear();
        }
        else
        {
            currentAction();
        }
    }

    private void InitConsole()
    {
        _debugString = string.Empty;
        _currentCommandLine.Clear();
        _stepsCount = -1;
        _isActive = false;
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
        _debugString = "programm was stopped on command " + _currentCommandLine.Last().StringValue;
        Debug.Log("programm was stopped on command " + _currentCommandLine.Last().StringValue);
    }

    private void Activate()
    {
        if (_isActive)
        {
            _debugString = "ERROR, you already activated robot!";
            Debug.Log("ERROR, you already activated robot!");
        }
        else
        {
            Debug.Log("Activated");
            _isActive = true;
        }
    }

    private void Disactivate()
    {
        if (_isActive)
        {
            Debug.Log("Disactivated");
            _isActive = false;
        }
        else
        {
            _debugString = "You need to activate robot first";
            Debug.Log("You need to activate robot first");
        }
    }

    private void RotateLeft()
    {
        Debug.Log("Rotate left");
    }

    private void RotateRight()
    {
        Debug.Log("Rotate right");
    }

    private void Move()
    {
        Debug.Log("Move");
        _currentCommandLine.Clear();
    }
}
