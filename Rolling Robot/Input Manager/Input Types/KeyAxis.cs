using Godot;
using System;

public partial class KeyAxis : InputType<float>
{
    [Export] Key PositiveKey;
    bool PositivePressed = false;
    [Export] Key NegativeKey;
    bool NegativePressed = false;

    float InputData = 0.0f;

    public override void UpdateInputData(InputEvent inputEvent)
    {
        if(inputEvent is InputEventKey keyEvent) {
            if(keyEvent.IsPressed()) {
                if(keyEvent.Keycode == PositiveKey) PositivePressed = true;
                if(keyEvent.Keycode == NegativeKey) NegativePressed = true;
            }

            if(keyEvent.IsReleased()) {
                if(keyEvent.Keycode == PositiveKey) PositivePressed = false;
                if(keyEvent.Keycode == NegativeKey) NegativePressed = false;
            }

            InputData = (PositivePressed ? 1.0f : 0.0f) - (NegativePressed ? 1.0f : 0.0f);
        }
    }

    public override float GetData() => InputData;
}
