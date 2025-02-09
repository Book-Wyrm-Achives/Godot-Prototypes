using Godot;
using System;

public partial class ObsticalSpawner : Node2D
{
    [Export] PackedScene ObsticalScene;
    [Export] float SpawnInterval;
    float spawnTime = 0.0f;


    public override void _Ready()
    {
        GD.Randomize();
    }

    public override void _Process(double delta)
    {
        spawnTime -= (float)delta;
        if (spawnTime <= 0.0f)
        {
            spawnTime = SpawnInterval;
            SpawnObstical();
        }
    }

    public void SpawnObstical()
    {
        var obstical = ObsticalScene.Instantiate<Obstical>();
        AddChild(obstical);
        obstical.Initialize(GlobalPosition, GD.Randf(), 150f + GD.Randf() * 150f, 150f);
    }

    public void Reset() {
        foreach(var child in GetChildren()) {
            child.QueueFree();
        }
    }
}
