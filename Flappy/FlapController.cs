using Godot;
using System;

public partial class FlapController : Node2D
{
    [Export] float Gravity;
    [Export] float FlapVelocity;

    Vector2 Velocity;

    public override void _PhysicsProcess(double deltaTime) {
        Velocity += Vector2.Down * Gravity * (float)deltaTime;

        Position += Velocity * (float)deltaTime;
    }

    public override void _Input(InputEvent inputEvent)
    {
        if(inputEvent is InputEventKey keyEvent && keyEvent.Keycode == Key.Space && keyEvent.IsPressed()) {
            Velocity = new Vector2(Velocity.X, 0) + Vector2.Up * FlapVelocity;
        }
    }

    public void AreaEntered(Area2D area) {
        Position = new Vector2(Position.X, GetViewportRect().Size.Y/2.0f);
        Velocity = Vector2.Zero;
    }
}
