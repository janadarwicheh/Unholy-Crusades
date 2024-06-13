using Godot;
using System;
using Skull.Scenes.Entities.Parameters;

public partial class EnemyFlying : Entity
{
	public AnimationNodeStateMachinePlayback Anim;
	private Playeru? player;
	private Vector2 velocity;
	int direction = -1;
	public override void _Ready()
	{
		base._Ready();
		Parameters = new EntityHandler(8, 3, 1, 300, null, null);
		Anim =  GetNode<AnimationTree>("AnimationTree").Get("parameters/playback").As<AnimationNodeStateMachinePlayback>();
	}

	public void Turn()
	{
		if (player.GlobalPosition.X > GlobalPosition.X)
		{
			_sprite2D.FlipH = true;
			direction = 1;
		}
		else
		{
			_sprite2D.FlipH = false;
			direction = -1;
		}
	}
	
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		if (player is not null)
		{
			Velocity = Velocity.MoveToward(player.GlobalPosition, (float)(1000*delta*direction));
			MoveAndSlide();
			Turn();
		}
	}

	public void _on_detection_area_entered(Area2D area)
	{
		Anim.Travel("Fly");
		player = (Playeru)area.GetParent();
	}
}
