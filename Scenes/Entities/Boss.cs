using Godot;
using System;
using Skull.Scenes.Entities.Parameters;

public partial class Boss : enemy
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		Parameters = new EntityHandler(150, 10, 6, 250, null, null);
	}
}
