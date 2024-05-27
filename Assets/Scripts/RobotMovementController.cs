using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovementController : MonoBehaviour
{
    private Vector2 _coordinates;

    private void InitRobot(Vector2 startCoordinates)
    {
        _coordinates = startCoordinates;
    }


}
