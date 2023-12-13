using Godot;
using System;

public partial class world : Node
{
	CharacterBody2D player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetNode<CharacterBody2D>("Player/Player(no animation tree)");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (player.Position.Y > 335)
			player.GlobalPosition = new Vector2(221, 186);
	}
}
