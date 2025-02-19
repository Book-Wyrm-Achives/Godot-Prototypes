using Godot;
using System;
using BookWyrm.Input;

public partial class SimpleColorChanger : Node2D
{
    [Export] Color cTrue;
    [Export] Color cFalse;

    public override void _Process(double delta) {
        bool input = InputManager.GetInputData<bool>("TestButton");

        if(input) Modulate = cTrue;
        else Modulate = cFalse;
    }
}
