using System;

public sealed class JumpController
{
    private readonly JumpSettings _settings;

    private float _coyoteTimer;
    private float _jumpBufferTimer;

    private bool _jumpHeld;
    private bool _jumpActive;
    private bool _jumpCutRequested;

    public JumpController(JumpSettings settings)
    {
        _settings = settings
            ?? throw new ArgumentNullException(nameof(settings));
    }

    public void PressJump()
    {
        _jumpHeld = true;
        _jumpBufferTimer = _settings.JumpBufferTime;
    }

    public void ReleaseJump()
    {
        _jumpHeld = false;

        if (_jumpActive)
        {
            _jumpCutRequested = true;
        }
    }

    public JumpStartResult Process(
        float deltaTime,
        bool isGrounded)
    {
        if (deltaTime < 0f)
        {
            throw new ArgumentOutOfRangeException(nameof(deltaTime));
        }

        UpdateCoyoteTimer(deltaTime, isGrounded);

        if (CanStartJump())
        {
            StartJump();

            return new JumpStartResult(
                true,
                _settings.JumpVelocity);
        }

        UpdateJumpBufferTimer(deltaTime);

        return JumpStartResult.None;
    }

    public JumpCutResult TryGetJumpCut()
    {
        if (!_jumpCutRequested)
        {
            return JumpCutResult.None;
        }

        _jumpCutRequested = false;
        _jumpActive = false;

        return new JumpCutResult(
            true,
            _settings.JumpCutMultiplier);
    }

    private void UpdateCoyoteTimer(
        float deltaTime,
        bool isGrounded)
    {
        if (isGrounded)
        {
            _coyoteTimer = _settings.CoyoteTime;
            return;
        }

        _coyoteTimer -= deltaTime;

        if (_coyoteTimer < 0f)
        {
            _coyoteTimer = 0f;
        }
    }

    private void UpdateJumpBufferTimer(float deltaTime)
    {
        _jumpBufferTimer -= deltaTime;

        if (_jumpBufferTimer < 0f)
        {
            _jumpBufferTimer = 0f;
        }
    }

    private bool CanStartJump()
    {
        return _jumpBufferTimer > 0f
            && _coyoteTimer > 0f;
    }

    private void StartJump()
    {
        _jumpBufferTimer = 0f;
        _coyoteTimer = 0f;

        _jumpActive = true;

        _jumpCutRequested = !_jumpHeld;
    }
}