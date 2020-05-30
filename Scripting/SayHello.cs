using Godot;
using System;

public class SayHello : Panel
{
	public override void _Ready()
	{
		GetNode("Button")
			.Connect("pressed", this, nameof(_OnButtonPressed));
	}

	public void _OnButtonPressed()
	{
		GetNode<Label>("Label").Text = "HELLO!";
	}
}


