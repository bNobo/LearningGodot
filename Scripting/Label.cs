using Godot;
using System;

public class Label : Godot.Label
{
    private float _accum;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AddToGroup("enemies");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        _accum += delta;
        Text = _accum.ToString();
    }
}
