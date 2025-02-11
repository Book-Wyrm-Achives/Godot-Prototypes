using Godot;
using System;

namespace BookWyrm.Shapes;

public partial class LineShape2D : Node2D
{
    public const string PACKEDSCENE_PATH = "res://BookWyrm/Shapes/Line/Line.tscn";
    public const string SHADER_PATH = "res://Bookwyrm/Shapes/Line/Line.gdshader";

    Vector2 pointARef;
    [Export] Vector2 PointA { get => pointARef; set => UpdatePoints(value, PointB); }

    Vector2 pointBRef;
    [Export] Vector2 PointB { get => pointBRef; set => UpdatePoints(PointA, value); }

    float thicknessRef;
    [Export] float Thickness { get => thicknessRef; set => UpdateThickness(value); }

    Color colorRef;
    [Export] Color Color { get => colorRef; set => UpdateColor(value); }

    public void CreateUniqueShaderMaterial() {
        if (this is CanvasItem canvas) {
            var shaderMat = new ShaderMaterial();
            shaderMat.Shader = GD.Load<Shader>(SHADER_PATH);

            canvas.Material = shaderMat;
        }
    }

    public void UpdatePoints(Vector2 pointA, Vector2 pointB)
    {
        Vector2 scale = (pointB - pointA).Abs() + Vector2.One;
        Vector2 pos = new Vector2(MathF.Min(pointA.X, pointB.X), MathF.Min(pointA.Y, pointB.Y));
        GD.Print(pos);

        pointARef = pointA;
        pointBRef = pointB;

        Scale = scale;
        Position = pos;

        if (this is CanvasItem canvas && canvas.Material is ShaderMaterial shaderMat)
        {
            shaderMat.SetShaderParameter("AspectRatio", scale);
            shaderMat.SetShaderParameter("ReferenceStartPoint", (pointA - pos) / Scale);
            shaderMat.SetShaderParameter("ReferenceEndPoint", (pointB - pos) / Scale);
        }
    }

    public void UpdateThickness(float value)
    {
        thicknessRef = value;

        if (this is CanvasItem canvas && canvas.Material is ShaderMaterial shaderMat)
        {
            shaderMat.SetShaderParameter("Thickness", value);
        }
    }

    public void UpdateColor(Color value)
    {
        colorRef = value;

        if (this is CanvasItem canvas && canvas.Material is ShaderMaterial shaderMat)
        {
            shaderMat.SetShaderParameter("Color", value);
        }
    }

    public static LineShape2D CreateLine(Node parent, Vector2 pointA, Vector2 pointB, float thickness, Color color, string path = PACKEDSCENE_PATH)
    {
        if(GD.Load(path) is PackedScene scene && scene.Instantiate() is LineShape2D line) {
            parent.AddChild(line);
            line.CreateUniqueShaderMaterial();
            line.UpdatePoints(pointA, pointB);
            line.UpdateThickness(thickness);
            line.UpdateColor(color);

            return line;
        }

        throw new Exception($"LineShape2D not found at path \"{path}\""); 
    }
}
