using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class InputValuesMatcher : MonoBehaviour
{
    [SerializeField]
    private List<TMP_InputField> _inputFields = new();

    [SerializeField]
    private List<string> _correctStringValues = new();

    [SerializeField]
    private float _correctNumValue;

    [SerializeField]
    private bool _isString;

    [SerializeField]
    private TextMeshProUGUI _answer;
    public bool IsAnswerCorrect;

    public void CountAnswer()
    {
        string answerString = string.Empty;
        int answerInt = 0;

        IsAnswerCorrect = true;

        if (_isString)
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

        Debug.Log("Значения правильные? - " + IsAnswerCorrect);

        //  return _isAnswerCorrect;
    }
}