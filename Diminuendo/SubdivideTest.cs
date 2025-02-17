using Godot;
using System;
using System.Collections.Generic;
using BookWyrm.Geometry;

[Tool]
public partial class SubdivideTest : Node2D
{
    [Export] Polygon2D Fill;

    [Export] Node2D A;
    [Export] Node2D B;

    [Export]
    bool TriggerSubdivide
    {
        get => false; set
        {
            if (value) Subdivide();
        }
    }

    public override void _Process(double delta)
    {
        QueueRedraw();
    }

    public override void _Draw()
    {
        if (A != null && B != null)
            DrawLine(A.Position, B.Position, Colors.Red, 5f);
    }

    public void Subdivide()
    {
        Vector2[] vertices = new Vector2[Fill.Polygon.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = Fill.Polygon[i] + Fill.Position; // Vertices are relative to parent
        }

        Line line = new Line(A.Position, B.Position);

        List<int> intersectionIndeces = new List<int>();
        List<Vector2> intersectionPoints = new List<Vector2>();

        for (int i = 0; i < vertices.Length; i++)
        {
            Line edge = new Line(vertices[i], vertices[(i + 1) % vertices.Length]);

            if (Line.Intersect(line, edge, out var intersectionPoint))
            {
                intersectionIndeces.Add(i);
                intersectionPoints.Add(intersectionPoint);
                GD.Print($"{i} - {intersectionPoint}");
            }
        }

        return;

        int polyCount = intersectionIndeces.Count;
        List<List<Vector2>> polys = new List<List<Vector2>>();

        if (polyCount % 2 != 0 || polyCount <= 1)
        {
            GD.PrintErr("Invalid line to subdivide");
            return;
        }

        for (int i = 0; i < polyCount; i++) polys.Add(new List<Vector2>());

        int currPoly = 0;

        for (int i = 0; i <= vertices.Length; i++)
        {
            if (i <= intersectionIndeces[currPoly])
            {
                polys[currPoly].Add(vertices[i]);
            }
            else
            {
                polys[currPoly].Add(intersectionPoints[currPoly]);
                currPoly = (currPoly + 1) % polyCount;
                polys[currPoly].Add(intersectionPoints[currPoly - 1]);
            }
        }

        Fill.Polygon = polys[0].ToArray();
    }
}
