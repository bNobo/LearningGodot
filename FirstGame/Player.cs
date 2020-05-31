using Godot;
using System;

public class Player : Area2D
{
    [Signal]
    public delegate void Hit();

    [Export]
    public int Speed = 400;

    private Vector2 _screenSize;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _screenSize = GetViewport().Size;
        Hide();
        Connect("body_entered", this, nameof(OnBodyEntered));
    }

    public void OnBodyEntered(PhysicsBody2D body)
    {
        Hide();
        EmitSignal(nameof(Hit));
        // We need to disable the player’s collision so that we don’t trigger the hit signal more than once.
        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
    }

    public void Start(Vector2 pos)
    {
        Position = pos;
        Show();
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        var velocity = new Vector2();

        if (Input.IsActionPressed("ui_right"))
        {
            velocity.x += 1;
        }

        if (Input.IsActionPressed("ui_left"))
        {
            velocity.x -= 1;
        }

        if (Input.IsActionPressed("ui_down"))
        {
            velocity.y += 1;
        }

        if (Input.IsActionPressed("ui_up"))
        {
            velocity.y -= 1;
        }

        var animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");

        if (velocity.Length() > 0)
        {
            velocity = velocity.Normalized() * Speed;
            animatedSprite.Play();
        }
        else 
        {
            animatedSprite.Stop();
        }

        Position += velocity * delta;

        Position = new Vector2
        (
            x: Mathf.Clamp(Position.x, 0, _screenSize.x),
            y: Mathf.Clamp(Position.y, 0, _screenSize.y)
        );

        if (velocity.x != 0)
        {
            animatedSprite.Animation = "walk";
            animatedSprite.FlipV = false;
            animatedSprite.FlipH = velocity.x < 0;
        }
        else    
        {
            animatedSprite.Animation = "up";
            animatedSprite.FlipV = velocity.y > 0;
        }
    }
}
