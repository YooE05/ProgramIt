using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class RobotMovement : MonoBehaviour
{
    public bool IsActive;
    public Vector2 Coordinates { get; private set; }
    private Rotation _currentRotation;

    [SerializeField]
    private RobotViewController _robotViewController;
    [SerializeField]
    private Transform _robotTransform;

    [SerializeField]
    private Grid _grid;

    public void InitRobot()
    {
        IsActive = false;

        var startCell = _grid.GetCellProperties(Coordinates);
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
        var nextCellCoordinates = Coordinates;
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
            SetNewCoordinates(nextCellCoordinates);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TryToPullLever()
    {
        var nextCellCoordinates = Coordinates;
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

        if (nextCell.GetType().Name == "LeverCell")
        {
            ((LeverCell)nextCell).AddPullCount();
            return true;
        }
        else
        {
            return false;
        }

        /*  if (nextCell.IsAbleToMove)
          {
              TranslateRobot(nextCell.Position);
              SetNewCoordinates(nextCellCoordinates);
              return true;
          }
          else
          {
              return false;
          }*/
    }


    public void SetNewCoordinates(Vector2 newCoordinates)
    {
        Coordinates = newCoordinates;
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