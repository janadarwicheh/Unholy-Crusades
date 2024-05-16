using Godot;
using System;
using System.Net.Http.Headers;
using Skull.Scenes.Entities.Parameters;

public partial class HurtboxEnemy : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public HurtboxEnemy()
	{
		CollisionLayer = 4;
		CollisionMask = 8;
	}
	
	public override void _Ready()
	{
		AreaEntered += OnAreaEntered;
		GD.Print(CollisionLayer, CollisionMask);
	}

	private void OnAreaEntered(Area2D area)
	{
		GD.Print(Owner.Name+" Area Entered HurtBox Enemy by " + area.Name);
		((Entity)(area.Owner)).TakeDamage((Entity)Owner, 0, 1);
	}
}
