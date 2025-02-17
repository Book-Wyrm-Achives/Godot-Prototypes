using Godot;
using System;
using BookWyrm.Geometry;

public partial class BallController : Node2D
{

    [Export] float Speed;
    [Export] Vector2 Direction;
    [Export] ShapeCast2D Cast;
    [Export] CollisionObject2D SelfCollider;

    public override void _Ready() {
        Cast.AddException(SelfCollider);
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 nextPosition = Position + Direction.Normalized() * Speed * (float) delta;
        Cast.TargetPosition = nextPosition - Position;
        Cast.ForceShapecastUpdate();
        if(Cast.GetCollisionCount() > 0) {
            Bounce(Cast.GetCollisionNormal(0));
        }

        Position += Direction.Normalized() * Speed * (float) delta;
    }

    public void Bounce(Vector2 normal) {
        Direction = -new Vector(Direction.X, Direction.Y).Reflection(normal);
    }
}
