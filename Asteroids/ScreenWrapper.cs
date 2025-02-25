using Godot;
using System;

public partial class ScreenWrapper : Node
{
    [Export] Vector2 Margin;
    Rect2 ViewportRect;
    Vector2 Position { get => GetParent<Node2D>().Position; set => GetParent<Node2D>().Position = value;}

    public override void _Ready()
    {
        ViewportRect = GetParent().GetViewport().GetVisibleRect();
    }

    public override void _PhysicsProcess(double delta)
    {
        float startX = ViewportRect.Position.X - Margin.X;
        float endX = ViewportRect.End.X + Margin.X;

        float startY = ViewportRect.Position.Y - Margin.Y;
        float endY = ViewportRect.End.Y + Margin.Y;

        if(Position.X < startX) Position = new Vector2(endX, Position.Y);
        if(Position.X > endX)   Position = new Vector2(startX, Position.Y);

        if(Position.Y < startY) Position = new Vector2(Position.X, endY);
        if(Position.Y > endY)   Position = new Vector2(Position.X, startY);
    }
}
