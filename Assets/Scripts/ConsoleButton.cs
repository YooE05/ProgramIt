using UnityEngine;

public class ConsoleButton : MonoBehaviour
{
    [SerializeField]
    private ButtonInputValues _value;

    public ButtonInputValues Value { get => _value; }
}
