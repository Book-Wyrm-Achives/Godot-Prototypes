using Godot;
using System;

public partial class MoveLeft : Node2D
{
    [Export] float Speed;

    public override void _PhysicsProcess(double delta)
    {
        Position += Vector2.Left * Speed * (float)delta;
    }
}
