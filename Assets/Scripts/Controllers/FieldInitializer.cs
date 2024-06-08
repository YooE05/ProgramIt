using UnityEngine;

public class FieldInitializer : MonoBehaviour
{
    [SerializeField]
    private Grid _grid;
    [SerializeField]
    private RobotMovement _robotMovement;

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
        //���������� ������ ������� ��� ������ � ����������� �� �������� ���������
        var startCell = _grid.GetCellProperties(_startCoordinates);
        startCell.SetSprite(_spriteForStartPosition);

        var winCell = _grid.GetCellProperties(WinCoordinates);
        winCell.SetSprite(_spriteForWinPosition);
        //�������� ������ �� ���� ��������� �������, ����� ���������� �� �������

        //�������� ��� ������ ���� ������������ ������

        //��������� ������
        _robotMovement.SetNewCoordinates(_startCoordinates);
        _robotMovement.InitRobot();
    }

    /*public void RestartFieldView()
    {

    }*/


}
