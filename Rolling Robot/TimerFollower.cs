using Godot;
using System;

public partial class TimerFollower : RichTextLabel
{
    [Export] BookWyrm.Utility.TimerNode timer;

    public override void _Process(double delta)
    {
        Text = $"{timer.Countdown} / {timer.Duration}";
    }
}
