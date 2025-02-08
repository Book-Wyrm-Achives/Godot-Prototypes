using Godot;
using System;

public partial class CameraController : Camera3D
{
    [Export] float MoveSpeed;
    [Export] float RotateSpeed;

    enum MoveKey
    {
        Forward,
        Backward,
        Left,
        Right,
        Up,
        Down
    }
    bool[] MoveKeys = new bool[6];

    enum RotKey {
        PitchUp,
        PitchDown,
        YawLeft,
        YawRight,
        RollLeft,
        RollRight
    }
    bool[] RotKeys = new bool[6];
    Vector3 MoveInput => new Vector3(
        (MoveKeys[(int)MoveKey.Right] ? 1 : 0) - (MoveKeys[(int)MoveKey.Left] ? 1 : 0),
        (MoveKeys[(int)MoveKey.Up] ? 1 : 0) - (MoveKeys[(int)MoveKey.Down] ? 1 : 0),
        (MoveKeys[(int)MoveKey.Backward] ? 1 : 0) - (MoveKeys[(int)MoveKey.Forward] ? 1 : 0)
    );

    Vector3 RotationInput => new Vector3(
        (RotKeys[(int)RotKey.YawRight] ? 1 : 0) - (RotKeys[(int)RotKey.YawLeft] ? 1 : 0),
        (RotKeys[(int)RotKey.PitchDown] ? 1 : 0) - (RotKeys[(int)RotKey.PitchUp] ? 1 : 0),
        (RotKeys[(int)RotKey.RollRight] ? 1 : 0) - (RotKeys[(int)RotKey.RollLeft] ? 1 : 0)
    );

    public override void _Process(double delta)
    {
        Position += (GlobalTransform.Basis.X * MoveInput.X + GlobalTransform.Basis.Y * MoveInput.Y + GlobalTransform.Basis.Z * MoveInput.Z) * MoveSpeed * (float)delta;
        RotateY(Mathf.DegToRad(RotationInput.X * RotateSpeed));
        RotateX(Mathf.DegToRad(RotationInput.Y * RotateSpeed));
        RotateZ(Mathf.DegToRad(RotationInput.Z * RotateSpeed));
    }

    bool Dragging = false;
    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent is InputEventKey keyEvent)
        {
            if (keyEvent.IsPressed())
            {
                switch (keyEvent.Keycode)
                {
                    case Key.W:     // Forward
                        MoveKeys[(int)MoveKey.Forward] = true;
                        break;
                    case Key.S:     // Backward
                        MoveKeys[(int)MoveKey.Backward] = true;
                        break;
                    case Key.A:     // Left
                        MoveKeys[(int)MoveKey.Left] = true;
                        break;
                    case Key.D:     // Right
                        MoveKeys[(int)MoveKey.Right] = true;
                        break;
                    case Key.Space: // Up
                        MoveKeys[(int)MoveKey.Up] = true;
                        break;
                    case Key.Shift: // Down
                        MoveKeys[(int)MoveKey.Down] = true;
                        break;
                    case Key.Q: // Yaw Left
                        RotKeys[(int)RotKey.YawLeft] = true;
                        break;
                    case Key.E: // Yaw Right
                        RotKeys[(int)RotKey.YawRight] = true;
                        break;
                    case Key.R: // Pitch Up 
                        RotKeys[(int)RotKey.PitchUp] = true;
                        break;
                    case Key.F: // Pitch Down
                        RotKeys[(int)RotKey.PitchDown] = true;
                        break;
                    case Key.Z: // Roll Left
                        RotKeys[(int)RotKey.RollLeft] = true;
                        break;
                    case Key.C: // Roll Right
                        RotKeys[(int)RotKey.RollRight] = true;
                        break;
                }
            }

            if (keyEvent.IsReleased())
            {
                switch (keyEvent.Keycode)
                {
                    case Key.W:     // Forward
                        MoveKeys[(int)MoveKey.Forward] = false;
                        break;
                    case Key.S:     // Backward
                        MoveKeys[(int)MoveKey.Backward] = false;
                        break;
                    case Key.A:     // Left
                        MoveKeys[(int)MoveKey.Left] = false;
                        break;
                    case Key.D:     // Right
                        MoveKeys[(int)MoveKey.Right] = false;
                        break;
                    case Key.Space: // Up
                        MoveKeys[(int)MoveKey.Up] = false;
                        break;
                    case Key.Shift: // Down
                        MoveKeys[(int)MoveKey.Down] = false;
                        break;
                    case Key.Q: // Yaw Left
                        RotKeys[(int)RotKey.YawLeft] = false;
                        break;
                    case Key.E: // Yaw Right
                        RotKeys[(int)RotKey.YawRight] = false;
                        break;
                    case Key.R: // Pitch Up
                        RotKeys[(int)RotKey.PitchUp] = false;
                        break;
                    case Key.F: // Pitch Down
                        RotKeys[(int)RotKey.PitchDown] = false;
                        break;
                    case Key.Z: // Roll Left
                        RotKeys[(int)RotKey.RollLeft] = false;
                        break;
                    case Key.C: // Roll Right
                        RotKeys[(int)RotKey.RollRight] = false;
                        break;
                }
            }
        }
    
        if(inputEvent is InputEventMouseButton mouseButtonEvent) {
            if(mouseButtonEvent.ButtonIndex == MouseButton.Left) {
                if(mouseButtonEvent.IsPressed()) {
                    Dragging = true;
                } else if(mouseButtonEvent.IsReleased()) {
                    Dragging = false;
                }
            }
        }
    }
}
