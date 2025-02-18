using Godot;
using System;

public partial class PlatformerController2D : Node2D
{
    [ExportCategory("Movement Parameters")]
    [ExportGroup("Gravity")]
    [Export] Vector2 GravityDirection;
    [Export] float GravityScale;
    public Vector2 GetGravity() => GravityDirection.Normalized() * GravityScale;

    [ExportGroup("Walk & -Run")]
    [Export] float MoveAcceleration;
    [Export] float MoveDecceleration;
    [Export] float MaxMoveSpeed;

    [ExportGroup("Jump")]
    [Export] float JumpSpeed;

    [ExportCategory("Sensors")]
    [Export] RayCast2D GroundSensor; // A raycast set up to have its base at the bottom of the player.

    #region Control Parameters
    public Vector2 Velocity;
    public Vector2 Acceleration;

    public bool Grounded;
    #endregion

    #region Input Parameters

    bool MoveRight, MoveLeft;
    float MoveX => (MoveRight ? 1.0f : 0.0f) - (MoveLeft ? 1.0f : 0.0f);
    
    bool JumpCancel;
    bool JumpInput;
    #endregion

    public override void _PhysicsProcess(double dt) => _PhysicsProcess((float)dt); // Me being very silly and not wanting to case dt every time
    public void _PhysicsProcess(float dt)
    {
        GroundCheck(dt, out var groundPosition);

        if (Grounded)
        {
            Velocity.Y = 0.0f;
            SnapToGround(groundPosition);
        }
        else
        {
            Acceleration += GetGravity();
        }

        ProcessInput(dt);


        Velocity = Velocity + Acceleration * dt; // For now, platformer is massless, so Force = Acceleration
        Position = Position + Velocity * dt;
        Acceleration = Vector2.Zero;
    }

    public void GroundCheck(float dt) => GroundCheck(dt, out var groundPosition);
    public void GroundCheck(float dt, out Vector2 groundPosition)
    {
        GroundSensor.TargetPosition = Vector2.Down * Mathf.Max(0.0f, Velocity.Y) * dt;
        GroundSensor.ForceRaycastUpdate();

        if (GroundSensor.IsColliding())
        {
            groundPosition = GroundSensor.GetCollisionPoint();
            Grounded = true;
        }
        else
        {
            groundPosition = Vector2.Zero;
            Grounded = false;
        }
    }

    public void SnapToGround(Vector2 groundPosition)
    {
        Vector2 groundDelta = groundPosition - GroundSensor.GlobalPosition;
        Position += groundDelta;
    }

    public void ProcessInput(float dt)
    {
        var xVel = MathF.Abs(Velocity.X);
        if (Mathf.Abs(MoveX) > 1e-6) // If MoveX is non-zero
        {
            if (xVel <= MaxMoveSpeed - MoveAcceleration * dt) // If can apply full acc, do so
            {
                Acceleration += Vector2.Right * MoveX * MoveAcceleration;
            }
            else if (xVel <= MaxMoveSpeed) // If below max, snap to max
            {
                Velocity.X = Mathf.Sign(MoveX) * MaxMoveSpeed;
            }
        }
        else // Deccelerate towards 0
        {
            if(xVel >= MoveDecceleration * dt) {
                Acceleration += Vector2.Left * Mathf.Sign(Velocity.X) * MoveDecceleration;
            } else {
                Velocity.X = 0.0f;
            }
        }

        if(JumpInput && !JumpCancel && Grounded) {
            // Jump!
            Velocity.Y = Mathf.Sign(Vector2.Up.Y) * JumpSpeed;
        }
        JumpCancel = JumpInput;
    }

    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent is InputEventKey keyEvent)
        {
            if (keyEvent.IsPressed())
            {
                switch (keyEvent.Keycode)
                {
                    case Key.D:
                        MoveRight = true;
                        break;
                    case Key.A:
                        MoveLeft = true;
                        break;
                    case Key.Space:
                        JumpInput = true;
                        break;
                }
            }
            if (keyEvent.IsReleased())
            {
                switch (keyEvent.Keycode)
                {
                    case Key.D:
                        MoveRight = false;
                        break;
                    case Key.A:
                        MoveLeft = false;
                        break;
                    case Key.Space:
                        JumpInput = false;
                        break;
                }
            }
        }
    }
}
