using Godot;
using System;

using BookWyrm.Shapes;
using BookWyrm.Geometry;

public partial class Test : Node
{
    public override void _Ready() {
        PointShape2D.CreatePoint(this, new Vector2(100, 100), 15f, Colors.Red);
        LineShape2D.CreateLine(this, new Vector2(100, 100), new Vector2(200, 200), 15f, Colors.Blue);
        PointShape2D.CreatePoint(this, new Vector2(200, 200), 15f, Colors.Green);
    }
}