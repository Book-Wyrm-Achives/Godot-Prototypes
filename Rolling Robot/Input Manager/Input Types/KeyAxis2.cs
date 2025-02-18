using Godot;
using System;

public partial class KeyAxis2 : InputType<Vector2>
{
    [Export] Key UpKey      = Key.W;
    bool UpPressed = false;
    [Export] Key DownKey    = Key.S;
    bool DownPressed = false;
    [Export] Key LeftKey    = Key.A;
    bool LeftPressed = false;
    [Export] Key RightKey   = Key.D;
    bool RightPressed = false;

    [Export] bool Normalize;
    Vector2 inputData = Vector2.Zero;

    public override void UpdateInputData(InputEvent inputEvent)
    {
        if (inputEvent is InputEventKey keyEvent)
        {
            if (keyEvent.IsPressed())
            {
                GD.Print($"{keyEvent.Keycode} was just pressed");
                if(keyEvent.Keycode == UpKey) UpPressed = true;
                if(keyEvent.Keycode == DownKey) DownPressed = true;

                if(keyEvent.Keycode == LeftKey) LeftPressed = true;
                if(keyEvent.Keycode == RightKey) RightPressed = true;
            }

            if(keyEvent.IsReleased()) {
                if(keyEvent.Keycode == UpKey) UpPressed = false;
                if(keyEvent.Keycode == DownKey) DownPressed = false;

                if(keyEvent.Keycode == LeftKey) LeftPressed = false;
                if(keyEvent.Keycode == RightKey) RightPressed = false;
            }

            inputData = (UpPressed ? 1.0f : 0.0f) * Vector2.Up
                + (DownPressed ? 1.0f : 0.0f) * Vector2.Down
                + (LeftPressed ? 1.0f : 0.0f) * Vector2.Left
                + (RightPressed ? 1.0f : 0.0f) * Vector2.Right;
                
        }
    }

    public override Vector2 GetData() => Normalize ? inputData.Normalized() : inputData;
}
