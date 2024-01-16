using Godot;
using System;
using System.ComponentModel;

public partial class Playeru : CharacterBody2D
{
	[Export]
	public const float Speed = 200.0f;
	[Export]
	public const float JumpVelocity = -200.0f;
	public Vector2 velocity;
	bool lock_anim = false;
	AnimatedSprite2D animation;
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	public bool canTakeDamage = true;
	public string state = "default";
	public void TakeDamage(double delta)
	{
		float direction = 1.0f;
		if (animation.FlipH)
		{
			direction = -1.0f;
		}
		velocity.X = -100.0f * direction * (float)delta;
		velocity.Y = -100.0f;
		state = "damaged";
	}
	private void update()
	{
		if(IsOnFloor())
			lock_anim = false;

		if(!lock_anim)
		{
			if(velocity.X>0)
			{
				animation.FlipH = false;
				animation.Play("run");
			}
			else if(velocity.X<0)
			{
				animation.FlipH = true;
				animation.Play("run");
			}
			else
			{
				animation.Play("idle");
			}
		}
	
		
	}
	private void Jump()
	{
		velocity.Y += JumpVelocity;
	}
	public override void _Ready()
	{
		animation = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}
	public override void _PhysicsProcess(double delta)
	{
		if(state == "default")
			{
			velocity = Velocity;
			// Add the gravity.
			if (!IsOnFloor())
			{
				lock_anim = true;
				velocity.Y += gravity * (float)delta;
				if (velocity.Y < -10)
					animation.Play("jump");
				else if (velocity.Y>10)
					animation.Play("fall");
				else 
					animation.Play("jump_to_fall");
			}
			else
				lock_anim = false;
				

			// Handle Jump.
			if (Input.IsActionJustPressed("jump") && IsOnFloor())
			{
				lock_anim = true;
				Jump();
			}

			// Get the input direction and handle the movement/deceleration.
			// As good practice, you should replace UI actions with custom gameplay actions.
			Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
			if (direction != Vector2.Zero)
			{
				velocity.X = direction.X * Speed;
			}
			else
			{
				velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			}
			Velocity = velocity;
			update();
			MoveAndSlide();
			for (int i = 0; i < GetSlideCollisionCount(); i++)
			{
				var collision = GetSlideCollision(i);
				var a = collision.GetCollider();
				if (a.GetType() == typeof(enemy))
				{
					TakeDamage(delta);
					animation.Play("hurt");
					Velocity = velocity;
					MoveAndSlide();
				}
			}
		}
		else
		{
			velocity.Y += gravity * (float)delta;
			Velocity = velocity;
			MoveAndSlide();
			animation.Play("hurt");
			if(IsOnFloor())
				state = "default";
		}
	}
}
