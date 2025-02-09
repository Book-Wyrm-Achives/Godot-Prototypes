using Godot;
using System;

public partial class Obstical : Node2D
{
    [Export] Node2D Top;
    [Export] Node2D Bottom;

    [Export] float Speed;

    public void Initialize(Vector2 spawnPoint, float windowCenter, float windowSize, float width) {
        Top.GlobalPosition = new Vector2(spawnPoint.X, GetViewportRect().Position.Y);
        Bottom.GlobalPosition = new Vector2(spawnPoint.X, GetViewportRect().End.Y);

        float topY = (windowCenter * GetViewportRect().Size.Y) - windowSize / 2.0f;
        float botY = GetViewportRect().Size.Y - ((windowCenter * GetViewportRect().Size.Y) + windowSize / 2.0f);
        

        Top.Scale = new Vector2(width, topY);
        Bottom.Scale = new Vector2(width, botY);
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += Vector2.Left * Speed * (float)delta;
        
    }
}
