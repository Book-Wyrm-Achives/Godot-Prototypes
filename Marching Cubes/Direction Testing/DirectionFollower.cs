using Godot;
using System;

[Tool]
public partial class DirectionFollower : Node3D
{
    [Export] Vector3 Offset = new Vector3(0, 0, 10);
    [Export] Node3D Target;

    public override void _Process(double deltaTime) {
        if(Target != null) {
            var targetPos = Target.GlobalTransform.Origin;
            var targetDir = Target.GlobalTransform.Basis.Z;
            Position = targetPos - targetDir;
        }
    }

    
}
