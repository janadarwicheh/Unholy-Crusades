using Godot;
using System;
using Skull.Scenes.Entities;
using Skull.Scenes.Entities.Parameters;

public partial class Hitbox : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public int SKillID;
	
	public Hitbox()
	{
		CollisionLayer = 8;
		CollisionMask = 0;
	}
}
