using Godot;

namespace Skull.Scenes.Entities.Parameters;

public partial class HurtboxEnemy : Godot.Area2D
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
	}

	private void OnAreaEntered(Godot.Area2D area)
	{
		GD.Print(Owner.Name+"'s HurtboxEnemy has been Entered by " + area.Name);
		((Entity)(Owner)).TakeDamage((Entity)area.Owner, 0, 1);
	}
}