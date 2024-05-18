using Godot;
using System;
using Skull.Scenes.Entities.Parameters;
using Skull.Scenes.Entities.Projetctiles;

public partial class MattBullet : Entity, IProjectile
{
	[Export] public float Speed = 50000;
	public float Direction;
	public void AreaEntered()
	{
		QueueFree();
	}

	public Vector2 SpawnPos;

	public float SpawnRot;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GlobalPosition = SpawnPos;
		GlobalRotation = SpawnRot;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Velocity = new Vector2(0, -Speed).Rotated(Direction);
		MoveAndSlide();
	}

	public void _on_timer_timeout()
	{
		QueueFree();
	}
}
