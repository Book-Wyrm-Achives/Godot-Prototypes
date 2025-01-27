using Godot;
using System;

[Tool]
public partial class Wall : Node2D
{
    [Export] public Vector2 Normal;
    [Export] bool Debug = false;

    public override void _Process(double deltaTime) {
        QueueRedraw();
    }

    public override void _Draw() {
        if(Debug)
            DrawLine(new Vector2(0, 0), Normal.Normalized() * 10, Colors.Green, 10);
    }
}
