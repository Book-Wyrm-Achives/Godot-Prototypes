using Godot;
using System;

public partial class BallCollision : Area2D
{
    [Signal] public delegate void WallCollidedEventHandler(Vector2 normal);
    [Signal] public delegate void BallCollidedEventHandler(Vector2 position);

    public void BallEntered(Area2D area) {
        if(area.GetParent() is Wall wall) {
            EmitSignal(SignalName.WallCollided, wall.Normal);
        }

        if(area.GetParent() is Ball ball) {
            ball.Bounce(GetParent<Node2D>().Position);
            EmitSignal(SignalName.BallCollided, ball.Position);
        }
    }
}
