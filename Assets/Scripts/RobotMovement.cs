using System.Collections.Generic;
using UnityEngine;

public sealed class RobotMovement : MonoBehaviour
{
    public bool IsActive;

    [SerializeField]
    private Vector2 _startCoordinates;

    private Vector2 _coordinates;
    private Rotation _currentRotation;

    [SerializeField]
    private RobotViewController _robotViewController;
    [SerializeField]
    private Transform _robotTransform;

    [SerializeField]
    private Grid _grid;

    [SerializeField]
    private Sprite _spriteForStartPosition;

    private void Start()
    {
        InitRobot();
    }

    public void InitRobot()
    {
        IsActive = false;

        _coordinates = _startCoordinates;

        var startCell = _grid.GetCellProperties(_coordinates);
        startCell.SetSprite(_spriteForStartPosition);
        _robotTransform.position = new Vector3(startCell.Position.x, startCell.Position.y, _robotTransform.position.z);

        _currentRotation = Rotation.Right;
        _robotViewController.SetActivateSprite(_currentRotation, IsActive);
    }

    public void RotateLeft()
    {
        switch (_currentRotation)
        {
            case Rotation.Right:
                _currentRotation = Rotation.Up;
                break;
            case Rotation.Down:
                _currentRotation = Rotation.Right;
                break;
            case Rotation.Left:
                _currentRotation = Rotation.Down;
                break;
            case Rotation.Up:
                _currentRotation = Rotation.Left;
                break;
            default:
                break;
        }

        _robotViewController.RotateRobotView(_currentRotation);
    }

    public void RotateRight()
    {
        switch (_currentRotation)
        {
            case Rotation.Right:
                _currentRotation = Rotation.Down;
                break;
            case Rotation.Down:
                _currentRotation = Rotation.Left;
                break;
            case Rotation.Left:
                _currentRotation = Rotation.Up;
                break;
            case Rotation.Up:
                _currentRotation = Rotation.Right;
                break;
            default:
                break;
        }

        _robotViewController.RotateRobotView(_currentRotation);
    }

    public bool TryMoveForward()
    {
        var nextCellCoordinates = _coordinates;
        switch (_currentRotation)
        {
            case Rotation.Right:
                nextCellCoordinates += Vector2.right;
                break;
            case Rotation.Down:
                nextCellCoordinates += Vector2.up;
                break;
            case Rotation.Left:
                nextCellCoordinates += Vector2.left;
                break;
            case Rotation.Up:
                nextCellCoordinates += Vector2.down;
                break;
            default:
                break;
        }

        var nextCell = _grid.GetCellProperties(nextCellCoordinates);
        if (nextCell.IsAbleToMove)
        {
            TranslateRobot(nextCell.Position);
            _coordinates = nextCellCoordinates;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void TranslateRobot(Vector2 needPosition)
    {
        _robotTransform.position = new Vector3(needPosition.x, needPosition.y, _robotTransform.position.z);
    }

    public void ChangeActivationStage(bool activationState)
    {
        IsActive = activationState;
        _robotViewController.SetActivateSprite(_currentRotation, IsActive);
    }

}

public enum Rotation
{
    Right = 0,
    Down = 1,
    Left = 2,
    Up = 3
}