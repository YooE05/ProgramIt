using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public sealed class ConsoleInputHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _consoleInputString;

    private List<Command> _commands = new();

    [SerializeField]
    private CommandParser _commandParser;

    public void AddButtonValue(ConsoleButton button)
    {
        //записать команду в строку
        if (_commands.Count > 0)
        {
            //если прошлая команда нуждается в продолжении, а текущая команда подходит под её стандарты продолжения, то всё нормально, но если это не так, то новую команду мы печатаем на новой строчке
            if (_commands.Last().NeedNextCommandInLine && !_commands.Last().IsNextCommandCorrect(button.Value))
            {
                GoToNextString();
            }
        }

        _commands.Add(new Command(button.Value));
        _consoleInputString.text += _commands.Last().StringValue;

        //если команда самостроятельная, то происходит переход на другую строку
        if (!_commands.Last().NeedNextCommandInLine)
        {
            GoToNextString();
        }
    }

    public void DeleteLastInput()
    {
        if (_commands.Count > 0)
        {
            //если последняя команда самостроятельная, то нужно ещё удалить символы перехода на строку
            if (_consoleInputString.text.Length > 4)
            {
                if (_consoleInputString.text.Substring(_consoleInputString.text.Length - 2, 2) == "\r\n")
                {
                    _consoleInputString.text = _consoleInputString.text.Substring(0, _consoleInputString.text.Length - 2);
                }
            }

            _consoleInputString.text = _consoleInputString.text.Substring(0, _consoleInputString.text.Length - (_commands.Last().StringValue.Length));

            if (_commands.Count > 1)
            {
                //если у нас перед текущим словом шло слово-начала команды, то мы не убираем переход на новую строку, а только стираем текущее слово
                if (_commands[_commands.Count - 2].NeedNextCommandInLine && !_commands[_commands.Count - 2].IsNextCommandCorrect(_commands.Last().ActionValue))
                {
                    _consoleInputString.text = _consoleInputString.text.Substring(0, _consoleInputString.text.Length - 2);
                }
            }

            _commands.Remove(_commands.Last());
        }
        else
        {
            ClearConsole();
        }
    }

    public void ClearConsole()
    {
        _consoleInputString.text = string.Empty;
        _commands.Clear();
    }

    public void StartProgram()
    {
        //было бы неплохо ещё отображать цветом ход по строчкам кода
        StopAllCoroutines();
        StartCoroutine(_commandParser.ReadProgram(_commands));

       // Debug.Log(debugMessage);
    }

    private void GoToNextString()
    {
        _consoleInputString.text += "\r\n";
    }
}

public sealed class Command
{
    public ButtonInputValues ActionValue;
    public string StringValue;
    //public bool HasPreviousCommand;
    public bool NeedNextCommandInLine;

    private List<ButtonInputValues> _acceptableNextCommands = new();

    public Command(ButtonInputValues actionValue)
    {
        ActionValue = actionValue;
        StringValue = GetButtonStringValue(actionValue);
        NeedNextCommandInLine = IsNeedNextCommand(actionValue);
    }

    private bool IsNeedNextCommand(ButtonInputValues actionValue)
    {
        bool needNextCommand;

        switch (actionValue)
        {
            case ButtonInputValues.Go:

                needNextCommand = true;
                //после команды ИДИ допустимы только цифры
                _acceptableNextCommands.Add(ButtonInputValues.Zero);
                _acceptableNextCommands.Add(ButtonInputValues.One);
                _acceptableNextCommands.Add(ButtonInputValues.Two);
                _acceptableNextCommands.Add(ButtonInputValues.Three);
                _acceptableNextCommands.Add(ButtonInputValues.For);
                _acceptableNextCommands.Add(ButtonInputValues.Five);
                _acceptableNextCommands.Add(ButtonInputValues.Six);
                _acceptableNextCommands.Add(ButtonInputValues.Seven);
                _acceptableNextCommands.Add(ButtonInputValues.Eight);
                _acceptableNextCommands.Add(ButtonInputValues.Nine);
                break;

            default:
                needNextCommand = false;
                break;
        }
        return needNextCommand;
    }

    public bool IsNumber()
    {
        bool isNumber;
        switch (ActionValue)
        {
            case ButtonInputValues.Zero:
                isNumber = true;
                break;
            case ButtonInputValues.One:
                isNumber = true;
                break;
            case ButtonInputValues.Two:
                isNumber = true;
                break;
            case ButtonInputValues.Three:
                isNumber = true;
                break;
            case ButtonInputValues.For:
                isNumber = true;
                break;
            case ButtonInputValues.Five:
                isNumber = true;
                break;
            case ButtonInputValues.Six:
                isNumber = true;
                break;
            case ButtonInputValues.Seven:
                isNumber = true;
                break;
            case ButtonInputValues.Eight:
                isNumber = true;
                break;
            case ButtonInputValues.Nine:
                isNumber = true;
                break;
            default:
                isNumber = false;
                break;
        }
        return isNumber;
    }

    private string GetButtonStringValue(ButtonInputValues emunValue)
    {
        string parsedSting = string.Empty;
        switch (emunValue)
        {
            case ButtonInputValues.Zero:
                parsedSting = "0";
                break;
            case ButtonInputValues.One:
                parsedSting = "1";
                break;
            case ButtonInputValues.Two:
                parsedSting = "2";
                break;
            case ButtonInputValues.Three:
                parsedSting = "3";
                break;
            case ButtonInputValues.For:
                parsedSting = "4";
                break;
            case ButtonInputValues.Five:
                parsedSting = "5";
                break;
            case ButtonInputValues.Six:
                parsedSting = "6";
                break;
            case ButtonInputValues.Seven:
                parsedSting = "7";
                break;
            case ButtonInputValues.Eight:
                parsedSting = "8";
                break;
            case ButtonInputValues.Nine:
                parsedSting = "9";
                break;
            case ButtonInputValues.Activate:
                parsedSting = "Включить";
                break;
            case ButtonInputValues.Disactivate:
                parsedSting = "Выключить";
                break;
            case ButtonInputValues.Right:
                parsedSting = "Поворот направо";
                break;
            case ButtonInputValues.Left:
                parsedSting = "Поворот налево";
                break;
            case ButtonInputValues.Go:
                parsedSting = "Иди ";
                break;
            default:
                break;
        }
        return parsedSting;
    }

    internal bool IsNextCommandCorrect(ButtonInputValues value)
    {
        for (int i = 0; i < _acceptableNextCommands.Count; i++)
        {
            if (value == _acceptableNextCommands[i])
            {
                return true;
            }
        }
        return false;
    }
}

public enum ButtonInputValues
{
    Zero = 0,
    One = 1,
    Two = 2,
    Three = 3,
    For = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,

    Activate = 10,
    Disactivate = 11,
    Right = 13,
    Left = 14,
    Go = 15
}
