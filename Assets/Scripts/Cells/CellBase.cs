using UnityEngine;

public class CellBase : MonoBehaviour
{
    [SerializeField]
    private Sprite _sprite;

    private SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _sprite;
    }

    public void SetSprite(Sprite sprite)
    {
        _sprite = sprite;
        _spriteRenderer.sprite = _sprite;
    }

    public Vector3 Position { get => transform.position; }

    public bool IsAbleToMove;

}
