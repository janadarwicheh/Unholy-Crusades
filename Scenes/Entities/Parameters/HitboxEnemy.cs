using Godot;
using System;
using Skull.Scenes.Entities.Parameters;
using Skull.Scenes.Entities.Projetctiles;

public partial class HitboxEnemy : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public HitboxEnemy()
	{
		CollisionLayer = 16;
		CollisionMask = 2;
	}
	
	public override void _Ready()
	{
		AreaEntered += OnAreaEntered;
		GD.Print(CollisionLayer, CollisionMask);
	}
	
	private void OnAreaEntered(Godot.Area2D area)
	{
		if (GetParent() is IProjectile)
		{
			GD.Print("bullet entered");
			((IProjectile)GetParent()).AreaEntered();
		}
		GD.Print(area.Name + " Area Entered HitBox enemy of "+ GetParent().Name);
		((Entity)(area.Owner)).TakeDamage((Entity)Owner, 0, 1);
	}
	
}
