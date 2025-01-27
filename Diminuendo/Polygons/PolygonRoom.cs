using Godot;
using System;

using BookWyrm.Geometry;
using System.Collections.Generic;

public partial class PolygonRoom : Node
{
    [Export] Vector2[] Vertices;
    [Export] Polygon2D polygon;

    public override void _Ready()
    {
        UpdateFill();
    }

    public void UpdateFill() {
        polygon.Polygon = Vertices;
    }

    public void Slice(Vector2 lineStart, Vector2 lineEnd) {
        
    }
}
