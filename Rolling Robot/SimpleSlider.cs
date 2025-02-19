using Godot;
using System;
using BookWyrm.Input;

public partial class SimpleSlider : Node2D
{
    [Export] float Speed;

    public override void _Process(double delta) {
        float moveInput = InputManager.GetInputData<float>("TestAxis");

        Position += Vector2.Right * moveInput * Speed * (float) delta;
    }
}
