using Godot;
using System;
using Skull.Scenes.Entities;
using Skull.Scenes.Entities.Parameters;
using Skull.Scenes.Entities.Projetctiles;
using Entity = Skull.Scenes.Entities.Parameters.Entity;

public partial class Hitbox : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public int SKillID;
	
	public Hitbox()
	{
		CollisionLayer = 8;
		CollisionMask = 4;
	}

	public override void _Ready()
	{
		Monitorable = true;
		Monitoring = true;
		AreaEntered += OnAreaEntered;
	}
	

	private void OnAreaEntered(Area2D area)
	{
		if (GetParent() is IProjectile)
		{
			GD.Print("bullet entered");
			((IProjectile)GetParent()).AreaEntered();
		}
		GD.Print(Owner.Name+" Area Entered HitBox "+ area.Name);
	}
}
