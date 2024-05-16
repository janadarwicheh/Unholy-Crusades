using Godot;
using System;
using Skull.Scenes.Entities;
using Skull.Scenes.Entities.Parameters;
using Entity = Skull.Scenes.Entities.Parameters.Entity;

public partial class Hitbox : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public int SKillID;
	
	public Hitbox()
	{
		CollisionLayer = 8;
		CollisionMask = 0;
	}

	public override void _Ready()
	{
		AreaEntered += OnAreaEntered;
		GD.Print(CollisionLayer, CollisionMask);
	}
	
	private void OnAreaEntered(Area2D area)
	{
		GD.Print(Owner.Name+" Area Entered HitBox "+ area.Name);
		((Entity)(area.Owner)).TakeDamage((Entity)Owner, 0, 1);
	}
}
