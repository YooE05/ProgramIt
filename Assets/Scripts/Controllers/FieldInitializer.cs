using System;
using UnityEngine;

public sealed class FieldInitializer : MonoBehaviour
{
    public Action OnFieldInited;

    [SerializeField]
    private Grid _grid;
    [SerializeField]
    private RobotMovement _robotMovement;
    [SerializeField]
    private LeversController _leversController;

    [SerializeField]
    private Vector2 _startCoordinates;
    [SerializeField]
    private Sprite _spriteForStartPosition;

    [field: SerializeField]
    public Vector2 WinCoordinates { get; private set; }

    [SerializeField]
    private Sprite _spriteForWinPosition;

    public void InitFieldView()
    {
        //установить нужные спрайты для клеток в зависимость от заданных координат
        var startCell = _grid.GetCellProperties(_startCoordinates);
        startCell.SetSprite(_spriteForStartPosition);

        var winCell = _grid.GetCellProperties(WinCoordinates);
        winCell.SetSprite(_spriteForWinPosition);
        //добавить проход по всем остальным клеткам, чтобы сбрасывать их спрайты

        //сбросить все стадии всех составляющих уровня

        //настроить робота
        _robotMovement.SetNewCoordinates(_startCoordinates);
        _robotMovement.InitRobot();
        _leversController.ClearPullsCount();
        //OnFieldInited?.Invoke();
    }

    /*public void RestartFieldView()
    {

    }*/


}
