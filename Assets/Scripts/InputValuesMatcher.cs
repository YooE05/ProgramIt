using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System;

public class InputValuesMatcher : MonoBehaviour
{
    public bool IsActiveCondition;

    [SerializeField]
    private FieldInitializer _fieldInitializer;

    [SerializeField]
    private List<TMP_InputField> _inputFields = new();

    [SerializeField]
    private List<string> _correctStringValues = new();

    [SerializeField]
    private float _correctNumValue;

    [SerializeField]
    private bool _isVariablesString;

    [SerializeField]
    private TextMeshProUGUI _answer;
    public bool IsAnswerCorrect;

    private void Awake()
    {
        if (IsActiveCondition)
        {
            _fieldInitializer.SetInactiveWinCell();
        }
        else
        {
            _fieldInitializer.SetActiveWinCell();
        }
    }

    public void CountAnswer()
    {
        string answerString = string.Empty;
        int answerInt = 0;

        IsAnswerCorrect = true;

        if (_isVariablesString)
        {
            if (_inputFields.Count == _correctStringValues.Count)
            {
                for (int i = 0; i < _inputFields.Count; i++)
                {
                    answerString += _inputFields[i].text.ToLower();
                    answerString += " ";

                    if (_inputFields[i].text.ToLower() != _correctStringValues[i].ToLower())
                    {
                        IsAnswerCorrect = false;
                    }
                }
            }
            else
            {
                Debug.Log("Не совпадает количество полей и их правильных значений");
            }

            _answer.text = answerString;
        }
        else
        {
            for (int i = 0; i < _inputFields.Count; i++)
            {
                //удалить пробелы и регистры
                int parsedNum;

                if (!int.TryParse(_inputFields[i].text, out parsedNum))
                {
                    IsAnswerCorrect = false;
                    _answer.text = "некорректный ввод значений";
                    break;
                }
                else
                {
                    if (i == 0)
                    {
                        answerInt += parsedNum;
                    }
                    else
                    {
                        answerInt -= parsedNum;
                    }
                }
            }
            if (IsAnswerCorrect)
            {
                _answer.text = answerInt.ToString();
                IsAnswerCorrect = (answerInt == _correctNumValue);
            }
        }


        if (IsActiveCondition)
        {
            if (IsAnswerCorrect)
            {
                _fieldInitializer.SetActiveWinCell();
            }
            else
            {
                _fieldInitializer.SetInactiveWinCell();
            }
        }

        Debug.Log("Значения правильные? - " + IsAnswerCorrect);
    }

    internal void ClearFields()
    {
        for (int i = 0; i < _inputFields.Count; i++)
        {
            _inputFields[i].text = string.Empty;
        }
    }
}