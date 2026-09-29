using UnityEngine;

public class PlayerVisualController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        if (_spriteRenderer == null)
        {
            Debug.LogError(
                $"{nameof(PlayerVisualController)}: Sprite Renderer is not assigned.",
                this);

            enabled = false;
        }
    }

    public void SetFacing(float direction)
    {
        if (Mathf.Approximately(direction, 0f))
        {
            return;
        }

        _spriteRenderer.flipX = direction < 0f;
    }
}
