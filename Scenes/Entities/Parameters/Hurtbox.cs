using Godot;
using System;
using Skull.Scenes.Entities.Parameters;

public partial class Hurtbox : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public Hurtbox()
	{
		CollisionLayer = 2;
		CollisionMask = 16;
	}
	
	public override void _Ready()
	{
		AreaEntered += OnAreaEntered;
		GD.Print(CollisionLayer, CollisionMask);
	}

	private void OnAreaEntered(Area2D area)
	{
		((Entity)(area.Owner)).TakeDamage((Entity)Owner, 0, 1);
	}
}
