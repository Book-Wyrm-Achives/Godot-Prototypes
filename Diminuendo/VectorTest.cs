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
        

        Vector v = new Vector(Vector.X, Vector.Y);
        Vector a = new Vector(Axis.X, Axis.Y);
        Vector projection = v.Projection(a);
        Vector rejection = v.Rejection(a);
        Vector reflection = v.Reflection(a);

        DrawVector(Axis.Normalized() * 100, Colors.Black);
        DrawVector(Vector, Colors.Blue);
        DrawVector(new Vector2(projection.X, projection.Y), Colors.Green);
        DrawVector(new Vector2(rejection.X, rejection.Y), Colors.Red);
        DrawVector(new Vector2(reflection.X, reflection.Y), Colors.Yellow);
        
        Vector rotated = v.Rotated2D(MathF.PI / 4);
        DrawVector(new Vector2(rotated.X, rotated.Y), Colors.Purple);

        GD.Print($"{Mathf.Abs(v.Magnitude() - rotated.Magnitude()) < 1e-6}: |{v.Magnitude()}-{rotated.Magnitude()}| = {Mathf.Abs(v.Magnitude() - rotated.Magnitude())}");
    }

    public void DrawVector(Vector2 vector, Color color) {
        DrawLine(Vector2.Zero, vector, color, 2);
        DrawCircle(vector, 3, color);
    }
}
