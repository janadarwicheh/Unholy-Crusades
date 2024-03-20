using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Skull.Scenes;
using Skull.Scenes.Entities;
using Skull.Scenes.Entities.Skills;
using Skull.Scenes.Entities.Stats;
using Resource = Skull.Scenes.Entities.Resources.Resource;

public partial class enemy : CharacterBody2D
{
	Area2D area_right;
	Area2D area_left;
	bool animationlock;
	public AnimatedSprite2D animation;
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
	public EntityComponent Parameters;
	public HitboxHandler Hitboxes;
	
	private void attack()
	{
		animation.Play("attack");
	}
	private void _on_animated_sprite_2d_frame_changed()
	{
		if (entered && animation.Animation == "attack" && (animation.Frame == 1 || animation.Frame == 2))
		{
			CurrentInfo.player.TakeDamage(frames,this);
		}
	}
	private void _on_area_2d_area_entered_right(Area2D area)
	{
		animation.FlipH = false;
		entered = true;
		animationlock = true;
		attack();
	}
	private void _on_area_2d_area_entered_left(Area2D area)
	{
		animation.FlipH = true;
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
		if (animation.Animation == "attack")
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
		animation = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		player = (Playeru)GetParent().GetParent().FindChild("Player(no animation tree)");
		Down[0] = GetNode<RayCast2D>("RayCast2DDownLeft");
		Down[1] = GetNode<RayCast2D>("RayCast2DDownRight");
		Side = GetNode<RayCast2D>("RayCast2DSide");
		Parameters = new EntityHandler(30, 4, 2, 250, null, null);
		Hitboxes = new HitboxHandler(this);
	}
	
	
	public override void _PhysicsProcess(double delta)
	{
		frames = delta;
		velocity = Velocity;
		if (shouldTurn())
			moveDir *= -1;
		if (moveDir == -1)
		{
			animation.FlipH = true;
		}
		else
		{
			animation.FlipH = false;
		}
		if (animation.FlipH)
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
			animation.Play("default");
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








