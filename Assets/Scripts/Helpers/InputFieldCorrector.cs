using TMPro;
using System;
using UnityEngine;

public class InputFieldCorrector : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _inputField;

    public void DeletAllSigns()
    {
        string tempString = _inputField.text;
        tempString = tempString.Trim(new Char[] { ' ', ',', '.' });

        if (tempString != _inputField.text)
        {
            _inputField.text = tempString;
        }
    }
}
