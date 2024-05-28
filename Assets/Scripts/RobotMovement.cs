using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class RobotMovement : MonoBehaviour
{
    public bool IsActive;

    private Vector2 _coordinates;
    private Rotation _currentRotation;

    [SerializeField]
    private RobotViewController _robotViewController;

    private void Start()
    {
        InitRobot();
    }

    private void InitRobot()
    {
        IsActive = false;
        //_coordinates = startCoordinates;
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

    public void MoveForward()
    {

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