using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public sealed class PlayerMovementController : MonoBehaviour
{
    [SerializeField]
    private MovementSettings _movementSettings;

    [SerializeField]
    private PlayerVisualController _visualController;

    [SerializeField]
    private PlayerInputReader _inputReader;

    [SerializeField]
    private GroundDetector _groundDetector;

    [SerializeField]
    private JumpSettings _jumpSettings;

    private Rigidbody2D _rigidbody;
    private PlayerMovement _movement;
    private JumpController _jumpController;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        if (!ValidateDependencies())
        {
            enabled = false;
            return;
        }

        _movement = new PlayerMovement(_movementSettings);
        _jumpController = new JumpController(_jumpSettings);
    }

    private void FixedUpdate()
    {
        UpdateGroundState();
        ProcessJumpInput();
        ProcessMovement();
    }

    private void UpdateGroundState()
    {
        _groundDetector.Refresh();
    }

    private void ProcessJumpInput()
    {
        if (_inputReader.ConsumeJumpPressed())
        {
            _jumpController.PressJump();
        }

        if (_inputReader.ConsumeJumpReleased())
        {
            _jumpController.ReleaseJump();
        }
    }

    private void ProcessMovement()
    {
        float horizontalVelocity = CalculateHorizontalVelocity();
        float verticalVelocity = CalculateVerticalVelocity();

        ApplyVelocity(
            horizontalVelocity,
            verticalVelocity);

        UpdateVisuals();
    }

    private float CalculateHorizontalVelocity()
    {
        return _movement.CalculateHorizontalVelocity(
            _rigidbody.linearVelocity.x,
            _inputReader.Move.x,
            Time.fixedDeltaTime);
    }

    private float CalculateVerticalVelocity()
    {
        JumpStartResult jumpStartResult = _jumpController.Process(
            Time.fixedDeltaTime,
            _groundDetector.IsGrounded);

        float verticalVelocity = _rigidbody.linearVelocity.y;

        if (jumpStartResult.IsTriggered)
        {
            verticalVelocity = jumpStartResult.VerticalVelocity;
        }

        return ApplyJumpCut(verticalVelocity);
    }

    private float ApplyJumpCut(float verticalVelocity)
    {
        JumpCutResult jumpCutResult =
            _jumpController.TryGetJumpCut();

        if (!jumpCutResult.IsTriggered)
        {
            return verticalVelocity;
        }

        if (verticalVelocity <= 0f)
        {
            return verticalVelocity;
        }

        return verticalVelocity * jumpCutResult.VelocityMultiplier;
    }

    private void ApplyVelocity(
        float horizontalVelocity,
        float verticalVelocity)
    {
        _rigidbody.linearVelocity = new Vector2(
            horizontalVelocity,
            verticalVelocity);
    }

    private void UpdateVisuals()
    {
        _visualController.SetFacing(
            _inputReader.Move.x);
    }

    private bool ValidateDependencies()
    {
        if (_movementSettings == null)
        {
            LogMissingDependency(nameof(_movementSettings));
            return false;
        }

        if (_jumpSettings == null)
        {
            LogMissingDependency(nameof(_jumpSettings));
            return false;
        }

        if (_inputReader == null)
        {
            LogMissingDependency(nameof(_inputReader));
            return false;
        }

        if (_visualController == null)
        {
            LogMissingDependency(nameof(_visualController));
            return false;
        }

        if (_groundDetector == null)
        {
            LogMissingDependency(nameof(_groundDetector));
            return false;
        }

        return true;
    }

    private void LogMissingDependency(string fieldName)
    {
        Debug.LogError(
            $"{nameof(PlayerMovementController)}: " +
            $"{fieldName} is not assigned.",
            this);
    }
}