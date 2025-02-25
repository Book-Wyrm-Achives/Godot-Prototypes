using Godot;
using System;

public partial class PlayerController : Node2D
{
    private const float DEG_TO_RAD = Mathf.Pi / 180.0f;

    [Export] float ThrustForce;
    [Export] float DragForce;
    [Export] float MaxSpeed;

    [Export] float TurnSpeed;

    Vector2 Forward => Vector2.Up.Rotated(Rotation);


    #region Control Parameters
    bool ThrustForwardPressed = false;
    bool ThrustBackwardPressed = false;
    float ThrustInput => (ThrustForwardPressed ? 1.0f : 0.0f) - (ThrustBackwardPressed ? 1.0f : 0.0f);

    bool LeftTurnPressed = false;
    bool RightTurnPressed = false;
    float TurnInput => (RightTurnPressed ? 1.0f : 0.0f) - (LeftTurnPressed ? 1.0f : 0.0f);

    #endregion

    public Vector2 Velocity;
    public Vector2 Acceleration;

    public float RotationVelocity;
    public float RotationAcceleration;

    public override void _PhysicsProcess(double deltaTime)
    {
        HandleThrustInput((float)deltaTime);
        HandleTurnInput((float) deltaTime);
        HandleMovement((float) deltaTime);
    }

    public void HandleMovement(float deltaTime) {
        Position += Velocity * deltaTime;
        Velocity += Acceleration * deltaTime;
        Acceleration = Vector2.Zero;

        Rotation += RotationVelocity * deltaTime;
        RotationVelocity += RotationAcceleration * deltaTime;
        RotationAcceleration = 0.0f;
    }

    public void HandleThrustInput(float deltaTime) {
        if (Mathf.Abs(ThrustInput) > 1e-6)
        {
            var speedInDirection = Velocity.Project(Forward).Length() * Mathf.Sign(Velocity.Dot(Forward));
            if(speedInDirection + ThrustForce * deltaTime < MaxSpeed) {
                Acceleration += Forward * ThrustForce * ThrustInput;
            }
            else if(speedInDirection < MaxSpeed) {
                Acceleration += Forward * (MaxSpeed - speedInDirection) / deltaTime * ThrustInput;
            }
        }
        else {
            if(Velocity.Length() - DragForce * deltaTime > 1e-6f) {
                Acceleration -= Velocity.Normalized() * DragForce;
            }
            else if(Velocity.Length() > 1e-6) {
                Acceleration -= Velocity / deltaTime;
            }
        }
    }

    public void HandleTurnInput(float deltaTime) {
        RotationVelocity = TurnInput * TurnSpeed * DEG_TO_RAD;
    }

    public override void _Input(InputEvent inputEvent) {
        if(inputEvent is InputEventKey keyEvent) {
            KeyToggle(keyEvent, Key.W, ref ThrustForwardPressed);
            KeyToggle(keyEvent, Key.S, ref ThrustBackwardPressed);
            KeyToggle(keyEvent, Key.D, ref RightTurnPressed);
            KeyToggle(keyEvent, Key.A, ref LeftTurnPressed);
        }
    }

    public static void KeyToggle(InputEventKey keyEvent, Key keycode, ref bool isPressed) {
        if(keyEvent.Keycode == keycode) {
            if(keyEvent.IsPressed()) isPressed = true;
            if(keyEvent.IsReleased()) isPressed = false;
        }
    }

    public static void MouseButtonToggle(InputEventMouseButton mouseButtonEvent, MouseButton button, ref bool isPressed) {
        if(mouseButtonEvent.ButtonIndex == button) {
            if(mouseButtonEvent.IsPressed()) isPressed = true;
            if(mouseButtonEvent.IsReleased()) isPressed = false;
        }
    }
}
