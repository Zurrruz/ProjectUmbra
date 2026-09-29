public readonly struct JumpCutResult
{
    public bool IsTriggered { get; }

    public float VelocityMultiplier { get; }

    public JumpCutResult(
        bool isTriggered,
        float velocityMultiplier)
    {
        IsTriggered = isTriggered;
        VelocityMultiplier = velocityMultiplier;
    }

    public static JumpCutResult None =>
        new(false, 1f);
}
