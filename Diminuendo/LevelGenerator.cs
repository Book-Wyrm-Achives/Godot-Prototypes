using Godot;
using System;
using System.Collections.Generic;
using BookWyrm.Geometry;
using System.Linq;

public partial class LevelGenerator : Node2D
{
    [Export] Node PointContainer;
    [Export] Polygon2D FillPolygon;
    [Export] Polygon2D ShellPolygon;
    [Export] CollisionPolygon2D ShellCollider;
    [Export] float ShellThickness;




    public override void _Ready()
    {
        GenerateFromPointContainer();
    }

    public void GenerateFromPointContainer() {
        Vector2[] points = new Vector2[PointContainer.GetChildCount()];

        for (int i = 0; i < points.Length; i++)
        {
            if (PointContainer.GetChild(i) is Node2D p)
            {
                points[i] = p.Position;
            }
            else GD.PrintErr("Nodes in PointContainer must be Node2Ds");
        }
        FillPolygon.Polygon = points;

        GenerateShell();
    }

    public void GenerateShell() {
        Vector2[] shell = WrapPolygon(FillPolygon.Polygon, ShellThickness);

        ShellPolygon.Polygon = shell;
        ShellCollider.Polygon = shell;
    }

    public static new Vector2[] Scale(Vector2[] vertices, float scalingFactor)
    {
        Vector2[] scaleVerts = new Vector2[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector v = vertices[i];
            Vector p = vertices[(i - 1 + vertices.Length) % vertices.Length];
            Vector n = vertices[(i + 1) % vertices.Length];

            Vector pv = v - p;
            Vector vn = n - v;
            Vector a = p + new Vector(-pv.Y, pv.X).Normalized() * scalingFactor;
            Vector b = n + new Vector(-vn.Y, vn.X).Normalized() * scalingFactor;

            Line.NearestPoint(Line.PointDirection(a, pv), Line.PointDirection(b, -vn), out var intersection, out var intersectionB);
            scaleVerts[i] = intersection;
        }

        return scaleVerts;
    }

    public static Vector2[] WrapPolygon(Vector2[] vertices, float thickness)
    {
        Vector2[] scaleVerts = Scale(vertices, thickness);
        Vector2[] wrapVerts = new Vector2[vertices.Length * 2 + 2];

        for (int i = 0; i < vertices.Length; i++)
        {
            wrapVerts[i] = vertices[i];
        }
        wrapVerts[vertices.Length] = vertices[0];
        wrapVerts[vertices.Length + 1] = scaleVerts[0];
        for (int i = 0; i < vertices.Length; i++)
        {
            wrapVerts[vertices.Length + i + 2] = scaleVerts[vertices.Length - i - 1];
        }

        return wrapVerts;
    }

    public List<List<Vector2>> SubdividePolygon(Vector2[] vertices, Line line)
    {

        List<int> intersections = new List<int>();
        List<Vector2> intersectionPoints = new List<Vector2>();

        for (int i = 0; i < vertices.Length; i++)
        {
            var edge = new Line(vertices[i], vertices[(i+1) % vertices.Length]);
            
            if (Line.Intersect(line, edge, out var intersectionPoint))
            {
                GD.Print($"{i} - {intersectionPoint}");
                if (edge.ContainsPoint(intersectionPoint) && line.ContainsPoint(intersectionPoint))
                {
                    intersections.Add(i);
                    intersectionPoints.Add(intersectionPoint);
                }
            }
        }

        int polyCount = intersections.Count;
        List<List<Vector2>> polys = new List<List<Vector2>>();

        if(polyCount <= 1) return polys;

        for(int i = 0; i < polyCount; i++) polys.Add(new List<Vector2>());

        int polyIndex = 0;
        for (int i = 0; i < vertices.Length; i++)
        {
            if(i < intersections[polyIndex]) {
                polys[polyIndex].Add(vertices[i]);
            } else {
                polys[polyIndex].Add(intersectionPoints[polyIndex]);
                int nextIndex = (polyIndex + 1) % polyCount;
                polys[nextIndex].Add(intersectionPoints[polyIndex]);
                polyIndex = nextIndex;
            }
        }

        return polys;
    }
}
