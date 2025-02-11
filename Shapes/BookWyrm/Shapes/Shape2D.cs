namespace BookWyrm.Shapes;
using Godot;

public static class Shape2D
{
    public static PointShape2D CreatePoint(Node parent, Vector2 point, float size, Color color, string path = PointShape2D.PACKEDSCENE_PATH)
        => PointShape2D.CreatePoint(parent, point, size, color, path);
    
    public static LineShape2D CreateLine(Node parent, Vector2 a, Vector2 b, float thickness, Color color, string path = LineShape2D.PACKEDSCENE_PATH)
        => LineShape2D.CreateLine(parent, a, b, thickness, color, path);
}
