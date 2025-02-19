using Godot;
using System;
using BookWyrm.Input;

public partial class SimpleMover : Node2D
{
    [Export] float Speed = 200f;

    public override void _Process(double deltaTime) {
        Vector2 moveInput = InputManager.GetInputData<Vector2>("TestAxis2");

        Position += moveInput.Normalized() * Speed * (float) deltaTime;
    }
}
