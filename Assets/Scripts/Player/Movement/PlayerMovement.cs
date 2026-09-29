using UnityEngine;

public class PlayerMovement
{
    private readonly MovementSettings _settings;

    public PlayerMovement(MovementSettings settings)
    {
        this._settings = settings;
    }

    public float CalculateHorizontalVelocity(
        float currentVelocity,
        float input,
        float deltaTime)
    {
        float targetVelocity = input * _settings.MaxSpeed;

        float accelerationRate = Mathf.Abs(targetVelocity) > 0.01f
            ? _settings.Acceleration
            : _settings.Deceleration;

        return Mathf.MoveTowards(
            currentVelocity,
            targetVelocity,
            accelerationRate * deltaTime);
    }    
}
