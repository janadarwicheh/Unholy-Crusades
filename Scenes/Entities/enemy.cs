using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Skull.Scenes;
using Skull.Scenes.Entities;
using Skull.Scenes.Entities.Skills;
using Skull.Scenes.Entities.Stats;
using Resource = Skull.Scenes.Entities.Resources.Resource;

public partial class enemy : Entity
{
	Area2D area_right;
	Area2D area_left;
	bool animationlock;
	public Playeru player;
	public bool entered = false;
	double frames;
	RayCast2D[] Down = new RayCast2D[2];
	RayCast2D Side;
	[Export]
	public int moveDir = 1;
	[Export]
	public int motionRange;
	[Export]
	public float speed = 250.0f;
	Vector2 velocity;
	public float gravity = CurrentInfo.gravity;
	public int KnockbackPower = 40000;
	
	private void attack()
	{
		Animation.Play("attack");
	}

	private void _on_attacked(Area2D area2D)
	{
		GD.Print("ATTACKED");
	}
	private void _on_animated_sprite_2d_frame_changed()
	{
		if (entered && Animation.Animation == "attack" && (Animation.Frame == 1 || Animation.Frame == 2))
		{
			CurrentInfo.player.TakeDamage(frames,this);
		}
	}
	private void _on_area_2d_area_entered_right(Area2D area)
	{
		Animation.FlipH = false;
		entered = true;
		animationlock = true;
		attack();
	}
	private void _on_area_2d_area_entered_left(Area2D area)
	{
		Animation.FlipH = true;
		animationlock = true;
		entered = true;
		attack();
	}
	private void _on_area_2d_right_area_exited(Area2D area)
	{
		entered = false;
	}
	
	private void _on_area_2d_left_area_exited(Area2D area)
	{
		entered = false;
	}
	private void _on_animated_sprite_2d_animation_finished()
	{
		if (Animation.Animation == "attack")
		{
			animationlock = false;
		}
	}
	
	private bool shouldTurn()
	{
		if (Side.IsColliding())
			return true;
		if (moveDir == 1)
			return !Down[1].IsColliding();
		if (moveDir == -1)
			return !Down[0].IsColliding();
		return false;
	}
	
	public override void _Ready()
	{
		velocity = Vector2.Zero;
		velocity.X = speed;
		area_right = GetNode<Area2D>("Area2DRight");
		area_left = GetNode<Area2D>("Area2DLeft");
		Animation = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		player = (Playeru)GetParent().GetParent().FindChild("Player(no Animation tree)");
		Down[0] = GetNode<RayCast2D>("RayCast2DDownLeft");
		Down[1] = GetNode<RayCast2D>("RayCast2DDownRight");
		Side = GetNode<RayCast2D>("RayCast2DSide");
		Parameters = new EntityHandler(30, 4, 2, 250, null, null);
	}
	
	
	public override void _PhysicsProcess(double delta)
	{
		frames = delta;
		velocity = Velocity;
		if (shouldTurn())
			moveDir *= -1;
		if (moveDir == -1)
		{
			Animation.FlipH = true;
		}
		else
		{
			Animation.FlipH = false;
		}
		if (Animation.FlipH)
		{
			area_right.Monitoring = false;
			area_left.Monitoring = true;
			Side.TargetPosition = new Vector2(-400, 0);
		}
		else
		{
			area_right.Monitoring = true;
			area_left.Monitoring = false;
			Side.TargetPosition = new Vector2(400, 0);
		}
		if (!animationlock)
		{
			Animation.Play("default");
			velocity.X = speed * moveDir;
		}
		else
		{
			velocity.X = 0;
		}
		velocity.Y += gravity * (float)delta;
		Velocity = velocity;
		MoveAndSlide();
	}
}








