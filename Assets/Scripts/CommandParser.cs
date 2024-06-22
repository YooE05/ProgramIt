using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

public sealed class CommandParser : MonoBehaviour
{
    public Action OnProgramStopped;
    public delegate void CommandAction();
    public CommandAction _currentAction;

    private List<Command> _commandsList = new();

    [SerializeField]
    private RobotMovement _robotMovement;
    [SerializeField]
    private FieldInitializer _fieldInitializer;

    private List<Command> _currentCommandLine = new();
    private int _stepsCount = -1;

    [SerializeField]
    private TextMeshProUGUI _debugString;
    private string _debugStringValue;
    private bool _needToStopProgram;

    public bool IsProgramCrashed { get => _needToStopProgram; }

    public IEnumerator ReadProgram(List<Command> inputCommands, List<Command> additionalCommands = null)
    {
        InitConsole();

        _commandsList = new List<Command>(inputCommands);

        _fieldInitializer.InitFieldView();
        //_robotMovement.InitRobot();

        if (additionalCommands != null)
        {
            _commandsList.AddRange(additionalCommands);
        }

        if (_commandsList.Count == 0)
        {
            StopProgramWithSpecialWarning("Нет команд");
        }

        for (int i = 0; i < _commandsList.Count; i++)
        {
            if (_needToStopProgram)
            {
                break;
            }

            _currentAction = DefineAction(_commandsList[i]);

            if (_inCycle && _hasOpenBrackets)
            {
                WtiteDownCommand(_commandsList[i]);
                yield return null;
            }
            else
            {
                if (i == 0 && _currentAction != Activate)
                {
                    StopProgramWithSpecialWarning("Сначала нужно включить робота");
                    break;
                }
                else if (_robotMovement.IsActive && i == _commandsList.Count - 1 && _currentAction != Disactivate || _commandsList.Count < 2)
                {
                    StopProgramWithSpecialWarning("Нужно отключить робота под конец");
                    break;
                }
                else if (i != 0 && !_robotMovement.IsActive)
                {
                    StopProgramWithSpecialWarning("Робот не может действовать после первого отключения");
                    break;
                }
                else
                {
                    //  RealiseAction(_currentAction, commands[i]);
                    yield return RealiseAction(_currentAction, _commandsList[i]);
                }
            }
        }

        if (_inCycle)
        {
            StopProgramWithSpecialWarning("Тело цикла некорректно");
        }


        //Сделать выводы после конца программы
        if (_debugStringValue == string.Empty)
        {
            _debugStringValue = "Команды успешно считаны";
        }

        Debug.Log(_debugStringValue);

        _debugString.text = _debugStringValue;

        OnProgramStopped?.Invoke();

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
            if (currentAction == StartCycle)
            {
                currentAction();
                yield return new WaitUntil(() => _cycleEnded == true);
                InitCycle();
            }
            else
            {
                currentAction();
                yield return new WaitForSeconds(1f);
            }
        }
    }

    private void InitConsole()
    {
        _commandsList.Clear();
        _needToStopProgram = false;
        _debugStringValue = string.Empty;
        _currentCommandLine.Clear();
        _stepsCount = -1;
        _robotMovement.IsActive = false;

        InitCycle();
    }

    private void InitCycle()
    {
        _inCycle = false;
        _hasOpenBrackets = false;
        _cycleEnded = false;
        _cycleIterationCount = 0;
        _cycleCommands.Clear();
    }

    private CommandAction DefineAction(Command command)
    {
        CommandAction returnedAction = StopProgram;

        _debugString.text = _cycleIterationCount > 0 ? "Идёт считвание тела цикла.\r\nТекущая команда - " + command.StringValue : "Текущая команда - " + command.StringValue;

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

           /* if (_currentCommandLine[0].ActionValue == ButtonInputValues.Pull && command.IsNumber())
            {
                returnedAction = Pull;
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
            }*/

            if (_currentCommandLine[0].ActionValue == ButtonInputValues.Do && command.IsNumber())
            {
                //returnedAction = RememberCycleActions;
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

                //Do();

                returnedAction = Do;//() => { Debug.Log("Запомнили количество итераций в цикле - " + _stepsCount); };
            }
        }
        else
        {
            switch (command.ActionValue)
            {
                case ButtonInputValues.Zero:
                    returnedAction = StopProgram;
                    break;
                case ButtonInputValues.One:
                    returnedAction = StopProgram;
                    break;
                case ButtonInputValues.Two:
                    returnedAction = StopProgram;
                    break;
                case ButtonInputValues.Three:
                    returnedAction = StopProgram;
                    break;
                case ButtonInputValues.For:
                    returnedAction = StopProgram;
                    break;
                case ButtonInputValues.Five:
                    returnedAction = StopProgram;
                    break;
                case ButtonInputValues.Six:
                    returnedAction = StopProgram;
                    break;
                case ButtonInputValues.Seven:
                    returnedAction = StopProgram;
                    break;
                case ButtonInputValues.Eight:
                    returnedAction = StopProgram;
                    break;
                case ButtonInputValues.Nine:
                    returnedAction = StopProgram;
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
                case ButtonInputValues.Pull:
                    // _currentCommandLine.Add(command);
                    // returnedAction = () => { Debug.Log("Waiting for number of pulls"); };
                    returnedAction = Pull;
                    break;
                case ButtonInputValues.Do:
                    if (_inCycle)
                    {
                        returnedAction = () => { Debug.Log("Ошибка! Цикл в цикле!"); };
                        StopProgramWithSpecialWarning("Ошибка! Цикл в цикле!");
                    }
                    else
                    {
                        returnedAction = () =>
                        {
                            Debug.Log("Waiting for cycle body");
                        };
                        _currentCommandLine.Add(command);
                    }
                    break;
                case ButtonInputValues.OpenBracket:
                    if (_inCycle && !_hasOpenBrackets)
                    {
                        returnedAction = OpenBrackets;
                    }
                    else
                    {
                        returnedAction = () => { Debug.Log(""); };
                        StopProgramWithSpecialWarning("Открывающая скобочка не к месту");
                    }
                    break;
                case ButtonInputValues.CloseBracket:
                    if (_inCycle && _hasOpenBrackets)
                    {
                        _inCycle = false;
                        returnedAction = StartCycle;
                    }
                    else
                    {
                        returnedAction = () => { Debug.Log(""); };
                        StopProgramWithSpecialWarning("Закрывабщая скобочка не к месту");
                    }
                    break;
                default:
                    returnedAction = () => { Debug.Log("incorrect command"); };
                    break;
            }
        }

        if (_inCycle)
        {
            _currentCommandLine.Clear();
        }
        return returnedAction;
    }

    private void OpenBrackets()
    {
        _hasOpenBrackets = true;
    }

    private void StopProgram()
    {
        _needToStopProgram = true;
        _debugStringValue = "Некорректная запись команды!";
        // Debug.Log("programm was stopped - incorrect line");
    }

    public void ResetRobot()
    {
        StopProgram();
        _fieldInitializer.InitFieldView();
        _debugStringValue = "Программа была остановлена";
    }

    private void StopProgramWithSpecialWarning(string warningText = "programm was stopped - incorrect line")
    {
        _needToStopProgram = true;
        _debugStringValue = warningText;
        Debug.Log(warningText);
    }

    private void Activate()
    {
        if (_robotMovement.IsActive)
        {
            StopProgramWithSpecialWarning("ERROR, you already activated robot!");
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
            StopProgramWithSpecialWarning("You need to activate robot first");
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
            //сделать свитч на разные варианты отказа идти вперёд
            StopProgramWithSpecialWarning("Робот не может пройти!");
        }

        _currentCommandLine.Clear();
    }

    private void Pull()
    {
        if (_robotMovement.TryToPullLever())
        {
            Debug.Log("Pull");
        }
        else
        {
            //сделать свитч на разные варианты отказа идти вперёд
            StopProgramWithSpecialWarning("Впереди нет рычага!");
        }

        _currentCommandLine.Clear();
    }

    private bool _inCycle;
    private bool _hasOpenBrackets;
    private bool _cycleEnded;
    private int _cycleIterationCount;
    private List<Command> _cycleCommands = new();


    private void Do()
    {
        _inCycle = true;
        _cycleEnded = false;

        _cycleIterationCount = _stepsCount;

        _currentCommandLine.Clear();
    }

    private void WtiteDownCommand(Command command)
    {
        _cycleCommands.Add(command);
    }

    private void StartCycle()
    {
        //_hasClosedBrackets = true;
        // _inCycle = false;
        //_cycleHasNullIteration = false;

        StartCoroutine(ReadCycleProgram());
    }

    IEnumerator ReadCycleProgram()
    {
        for (int i = 0; i < _cycleIterationCount; i++)
        {
            for (int k = 0; k < _cycleCommands.Count; k++)
            {
                if (_needToStopProgram)
                {
                    break;
                }

                _currentAction = DefineAction(_cycleCommands[k]);

                if (_currentAction == Activate)
                {
                    StopProgramWithSpecialWarning("Нельзя активировать повторно");
                    break;
                }
                else if (_currentAction == Disactivate && _cycleIterationCount > 1)
                {
                    StopProgramWithSpecialWarning("Робот не может действовать после первого отключения");
                    break;
                }
                else if (!_robotMovement.IsActive)
                {
                    StopProgramWithSpecialWarning("Робот не может действовать после первого отключения");
                    break;
                }
                else
                {
                    yield return RealiseAction(_currentAction, _cycleCommands[k]);
                }
            }

            if (_needToStopProgram)
            {
                break;
            }

        }
        _cycleEnded = true;
        yield return null;
    }

}
