using UnityEngine;
using System.Collections.Generic;

public class AdditionalProgrammCode : MonoBehaviour
{
    private List<Command> _commands = new();

    [SerializeField]
    private List<ButtonInputValues> _values = new();

    public List<Command> GetCommands()
    {
        CollectCommands();
        return _commands;
    }

    public virtual void CollectCommands()
    {
        for (int i = 0; i < _values.Count; i++)
        {
            _commands.Add(new Command(_values[i]));
        }
    }
}
