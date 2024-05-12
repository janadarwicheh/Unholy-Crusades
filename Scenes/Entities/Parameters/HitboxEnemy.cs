using Godot;
using System;
using Skull.Scenes.Entities.Parameters;

public partial class HitboxEnemy : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public HitboxEnemy()
	{
		CollisionLayer = 16;
		CollisionMask = 0;
	}
	
}
