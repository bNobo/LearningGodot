using Godot;
using System;

public class TimerExample : Node2D
{
    [Signal]
    public delegate void MySignal();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode("Timer").Connect("timeout", this, nameof(OnTimerTimeout));
    }

    public void OnTimerTimeout()
    {
        var sprite = GetNode<Sprite>("Sprite");
        sprite.Visible = !sprite.Visible;
        EmitSignal(nameof(MySignal));
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
