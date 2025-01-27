using Godot;
using System;
using BookWyrm.Geometry;

public partial class Ball : Node2D
{
    [Export] public Vector2 Direction = new Vector2(0, 1);
    [Export] public float Speed = 200;
    [Export] float BounceCooldown = 0.1f;
    float bounceTimer = 0.0f;

    Vector2 Velocity => Direction.Normalized() * Speed;



    public override void _Ready()
    {
        Direction = Vector2.FromAngle(Rotation);
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += Velocity * (float)delta;
        bounceTimer -= (float)delta;
    }

    public void Flip(Vector2 normal)
    {
        Vector norm = new Vector(normal.X, normal.Y);
        Vector along = norm.Rotated2D(MathF.PI / 2);
        Vector vel = new Vector(Velocity.X, Velocity.Y);

        vel = vel.Reflection(along);
        Direction = new Vector2(vel.X, vel.Y);
    }

    public void Bounce(Vector2 position)
    {
        if (bounceTimer > 0)
            return;

        Flip(Position - position);
        bounceTimer = BounceCooldown;
    }

    public void SetSpeed(float speed)
    {
        Speed = speed;
    }
}
