public readonly struct JumpResult
{
    public bool ShouldJump { get; }
    public float VerticalVelocity { get; }

    public JumpResult(
        bool shouldJump,
        float verticalVelocity)
    {
        ShouldJump = shouldJump;
        VerticalVelocity = verticalVelocity;
    }
}
