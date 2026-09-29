public readonly struct JumpStartResult
{
    public bool IsTriggered { get; }

    public float VerticalVelocity { get; }

    public JumpStartResult(
        bool isTriggered,
        float verticalVelocity)
    {
        IsTriggered = isTriggered;
        VerticalVelocity = verticalVelocity;
    }

    public static JumpStartResult None =>
        new(false, 0f);
}
