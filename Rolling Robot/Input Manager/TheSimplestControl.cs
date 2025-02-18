using Godot;
using System;

public partial class TheSimplestControl : Node2D
{
    [Export] float Speed;

    public override void _Process(double delta)
    {
        Vector2 moveInput = InputManager.GetData<Vector2>("Move");

        //GD.Print($"{moveInput}");
        
        Position += moveInput * Speed * (float) delta;
    }
}
