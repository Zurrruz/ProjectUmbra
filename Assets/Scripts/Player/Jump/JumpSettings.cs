using UnityEngine;

[CreateAssetMenu(
    fileName = "JumpSettings",
    menuName = "Game/Player/Jump Settings")]
public sealed class JumpSettings : ScriptableObject
{
    [field: SerializeField, Min(0f)]
    public float JumpVelocity { get; private set; } = 10f;

    [field: SerializeField, Min(0f)]
    public float CoyoteTime { get; private set; } = 0.1f;

    [field: SerializeField, Min(0f)]
    public float JumpBufferTime { get; private set; } = 0.1f;

    [field: SerializeField, Range(0f, 1f)]
    public float JumpCutMultiplier { get; private set; } = 0.5f;
}
