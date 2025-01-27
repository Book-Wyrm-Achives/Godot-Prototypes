using Godot;
using System;

public partial class BallSpeedController : Node
{
    [Export] Ball[] balls;

    public void SetSpeed(float speed)
    {
        foreach (var ball in balls)
        {
            ball.SetSpeed(speed);
        }
    }
}
