using UnityEngine;

[CreateAssetMenu(fileName = "MovementSettings", menuName = "Game/Player/Movement Settings")]

public class MovementSettings : ScriptableObject
{
    [SerializeField, Min(0f)]
    private float _maxSpeed = 6f;

    [SerializeField, Min(0f)]
    private float _acceleration = 30f;

    [SerializeField, Min(0f)]
    private float _deceleration = 40f;

    public float MaxSpeed => _maxSpeed;
    public float Acceleration => _acceleration;
    public float Deceleration => _deceleration;
}
