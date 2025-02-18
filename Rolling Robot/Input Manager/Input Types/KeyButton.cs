using Godot;
using System;

public partial class KeyButton : InputType<bool>
{
    [Export] Key ButtonKey;
    bool ButtonPressed = false;

    public override void UpdateInputData(InputEvent inputEvent) {
        if(inputEvent is InputEventKey keyEvent && keyEvent.Keycode == ButtonKey) {
            if(keyEvent.IsPressed())    ButtonPressed = true;
            if(keyEvent.IsReleased())   ButtonPressed = false;
        }
    }

    public override bool GetData() => ButtonPressed;
}
