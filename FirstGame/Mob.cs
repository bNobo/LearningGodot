using Godot;
using System;

public class Mob : RigidBody2D
{
    [Export]
    public int MinSpeed = 150;

    [Export]
    public int MaxSpeed = 250;

    static private Random _random = new Random();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        var mobTypes = animatedSprite.Frames.GetAnimationNames();

        animatedSprite.Animation = mobTypes[_random.Next(0, mobTypes.Length)];
        GetNode("VisibilityNotifier2D").Connect("screen_exited", this, nameof(OnScreenExited));
    }

    public void OnScreenExited()
    {
        QueueFree();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
