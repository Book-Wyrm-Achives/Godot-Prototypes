using Godot;
using System;
using BookWyrm.Geometry;


[Tool]
public partial class VectorTest : Node2D
{
    [Export] Vector2 Vector;
    [Export] Vector2 Axis;

    public override void _Process(double deltaTime) {
        Vector = GetViewport().GetMousePosition() - GlobalPosition;
        QueueRedraw();
    }
    public override void _Draw() {
        DrawLine(Vector2.Zero, Axis.Normalized() * 100, Colors.Red, 2);
        DrawCircle(Axis.Normalized() * 100, 2, Colors.Red);
        
        DrawLine(Vector2.Zero, Vector, Colors.Blue, 2);
        DrawCircle(Vector, 3, Colors.Blue);
    }
}
