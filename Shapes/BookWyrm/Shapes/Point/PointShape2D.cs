namespace BookWyrm.Shapes;
using BookWyrm.Geometry;
using Godot;
using System;

public partial class PointShape2D : Node2D
{
    public const string PACKEDSCENE_PATH = "res://BookWyrm/Shapes/Point/Point.tscn";
    public const string SHADER_PATH = "res://BookWyrm/Shapes/Point/Point.gdshader";

    private Color colorRef;
    public Color Color { get => colorRef; set => SetColor(value); }

    public void CreateUniqueShaderMaterial() {
        if (this is CanvasItem canvas) {
            var shaderMat = new ShaderMaterial();
            shaderMat.Shader = GD.Load<Shader>(SHADER_PATH);

            canvas.Material = shaderMat;
        }
    }

    public void SetColor(Color color) {
        if(this is CanvasItem canvas) {
            if(canvas.Material is ShaderMaterial shaderMat) {
                shaderMat.SetShaderParameter("Color", color);
            }
        }
    }

    public static PointShape2D CreatePoint(Node parent, Vector position, float radius, Color color, string path = PACKEDSCENE_PATH) {        
        if(GD.Load(path) is PackedScene scene && scene.Instantiate() is PointShape2D point) {
            parent.AddChild(point);
            point.CreateUniqueShaderMaterial();
            point.Position = position;
            point.Scale = Vector2.One * radius * 2.0f;
            point.Color = color;

            return point;
        }

        throw new Exception($"PointShape2D not found at path \"{path}\"");
    }
}