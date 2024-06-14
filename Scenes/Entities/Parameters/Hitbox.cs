using Godot;
using Skull.Scenes.Entities.Projetctiles;

namespace Skull.Scenes.Entities.Parameters;

public partial class Hitbox : Godot.Area2D
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
	

	private void OnAreaEntered(Godot.Area2D area)
	{
		if (GetParent() is IProjectile)
		{
			GD.Print("bullet entered");
			((IProjectile)GetParent()).AreaEntered();
		}
		GD.Print(Owner.Name+" Area Entered HitBox "+ area.Name);
	}
}