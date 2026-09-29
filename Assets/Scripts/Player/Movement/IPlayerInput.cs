using UnityEngine;

public interface IPlayerInput
{
    Vector2 Move { get; }

    bool ConsumeJumpPressed();

    bool ConsumeJumpReleased();
}