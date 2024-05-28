using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class RobotViewController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private List<Sprite> _idleSprites;

    public void RotateRobotView(Rotation _currentRotation)
    {
        Sprite _needSprite;
        switch (_currentRotation)
        {
            case Rotation.Right:
                _needSprite = _idleSprites[0];
                break;
            case Rotation.Down:
                _needSprite = _idleSprites[1];
                break;
            case Rotation.Left:
                _needSprite = _idleSprites[2];
                break;
            case Rotation.Up:
                _needSprite = _idleSprites[3];
                break;
            default:
                _needSprite = _idleSprites[0];
                break;
        }

        _spriteRenderer.sprite = _needSprite;
    }

    public void SetActivateSprite(Rotation _currentRotation, bool isActive)
    {
        Sprite _needSprite;

        if (isActive)
        {
            RotateRobotView(_currentRotation);
        }
        else
        {
            switch (_currentRotation)
            {
                case Rotation.Right:
                    _needSprite = _idleSprites[4];
                    break;
                case Rotation.Down:
                    _needSprite = _idleSprites[5];
                    break;
                case Rotation.Left:
                    _needSprite = _idleSprites[6];
                    break;
                case Rotation.Up:
                    _needSprite = _idleSprites[7];
                    break;
                default:
                    _needSprite = _idleSprites[4];
                    break;
            }
            _spriteRenderer.sprite = _needSprite;
        }        
    }

}
