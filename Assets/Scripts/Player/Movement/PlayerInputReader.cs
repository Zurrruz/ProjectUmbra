using UnityEngine;
using UnityEngine.InputSystem;

public sealed class PlayerInputReader : MonoBehaviour, IPlayerInput
{
    [SerializeField]
    private InputActionReference _moveAction;

    [SerializeField]
    private InputActionReference _jumpAction;

    private Vector2 _move;

    private bool _jumpPressed;
    private bool _jumpReleased;

    public Vector2 Move => _move;

    private void Awake()
    {
        if (_moveAction == null)
        {
            Debug.LogError(
                $"{nameof(PlayerInputReader)}: Move action is not assigned.",
                this);

            enabled = false;
            return;
        }

        if (_jumpAction == null)
        {
            Debug.LogError(
                $"{nameof(PlayerInputReader)}: Jump action is not assigned.",
                this);

            enabled = false;
        }
    }

    private void OnEnable()
    {
        if (_moveAction != null)
        {
            _moveAction.action.Enable();
        }

        if (_jumpAction != null)
        {
            _jumpAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (_moveAction != null)
        {
            _moveAction.action.Disable();
        }

        if (_jumpAction != null)
        {
            _jumpAction.action.Disable();
        }
    }

    private void Update()
    {
        _move = _moveAction.action.ReadValue<Vector2>();

        if (_jumpAction.action.WasPressedThisFrame())
        {
            _jumpPressed = true;
        }

        if (_jumpAction.action.WasReleasedThisFrame())
        {
            _jumpReleased = true;
        }
    }

    public bool ConsumeJumpPressed()
    {
        if (!_jumpPressed)
        {
            return false;
        }

        _jumpPressed = false;
        return true;
    }

    public bool ConsumeJumpReleased()
    {
        if (!_jumpReleased)
        {
            return false;
        }

        _jumpReleased = false;
        return true;
    }
}