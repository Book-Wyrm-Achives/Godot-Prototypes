using Godot;
using System;

public partial class TimerTest : Node
{
    [Export] float Duration;
    BookWyrm.Utility.Timer timer;

    [Signal] public delegate void CountdownBroadcastEventHandler(float countdown);

    public override void _Ready()
    {
        timer = new BookWyrm.Utility.Timer(Duration);
        timer.OnTimerComplete += OnTimerComplete;
    }

    public override void _Process(double delta)
    {
        timer.Update((float)delta);
        EmitSignal(SignalName.CountdownBroadcast, timer.Countdown);
    }

    public void OnTimerComplete() {
        GD.Print("Timer Completed");
    }

    public void ResetTimer() {
        timer.Reset();
    }

    public void OnTimerNodeComplete() {
        GD.Print("Timer Node Completed");
    }
}
