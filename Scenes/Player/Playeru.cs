using Godot;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Skull.Scenes;
using Skull.Scenes.Entities;
using Skull.Scenes.Entities.Skills;

public partial class Playeru : CharacterBody2D
{
	public float Speed { get; set; }
	[Export]
	public const float JumpVelocity = -225.0f * 30;
	public Vector2 velocity;
	bool lock_anim = false;
	AnimatedSprite2D animation;
	public Area2D Attack;
	public float gravity = CurrentInfo.gravity;
	public bool canTakeDamage = true;
	public string state = "default";
	public bool doubleJump = false;
	public EntityComponent Parameters { get; set; }
	public int knockback  = 200;
	public int Cooldown { get; set; } = 500000;
	public bool gotHit = false;
	public bool CanLaunch { get; set; } = true;
	public float DoubleJumpVelocity { get; } = -175 * 25;
	public HitboxHandler Hitboxes;
	
	
	public void StartCooldown()
	{
		if (CanLaunch)
		{
			Task.Delay(Cooldown).ContinueWith(t => gotHit = false);
		}
	}
	
	public void Hit()
	{
		
		if (gotHit)
		{
			SetCollisionLayerValue(2, false);
			SetCollisionLayerValue(4, false);
			StartCooldown();
			SetCollisionLayerValue(4, true);
			SetCollisionLayerValue(2, true);
		}
		
	}

	public void on_animation_finished()
	{
		if (animation.Animation == "Attack")
		{
			GD.Print("aa");
			state = "default";
			Attack.Monitoring = false;
		}
	}
	public void attack()
	{
		Attack.Monitoring = true;
		state = "attacking";
	}
	public void TakeDamage(double delta, enemy a)
	{
		
		var knockbackDirection = (a.Velocity)-velocity.Normalized() * knockback;
		velocity = knockbackDirection;
		if (a.Velocity.X ==0 && velocity.X ==0 )
		{
			if (a.animation.FlipH)
			{
				velocity.X = -(a.KnockbackPower);
			}
			else
			{
				velocity.X = (a.KnockbackPower);
			}
			
		}
		if (IsOnFloor())
		{
			velocity.Y = -2000;
			
		}
		
		MoveAndSlide();
		state = "damaged";
		gotHit = true;
		
	}
	
	private void update()
	{
		if(IsOnFloor())
			lock_anim = false;

		if (state == "attacking")
			animation.Play("Attack");
		else 
		{
			if (!lock_anim)
			{ 
				if (velocity.X > 0)
				{
					animation.FlipH = false;
					animation.Play("run");
				}
				else if (velocity.X < 0)
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
	
		
	}
	private void Jump()
	{
		velocity.Y += JumpVelocity;
	}
	private void DoubleJump()
	{
		if (!doubleJump)
			velocity.Y = DoubleJumpVelocity;
	}
	public override void _Ready()
	{
		animation = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		Speed = Parameters.CurrentStats[StatType.Speed].Amount * 40;
		GD.Print("Speed :" + Speed);
		Attack = GetNode<Area2D>("Attaque");
	}
	public override void _PhysicsProcess(double delta)
	{
		if(state == "default" || state == "attacking")
		{
			velocity = Velocity;
			// Add the gravity.
			if (Input.IsActionJustPressed("Attack") == (state != "attacking"))
			{
				attack();
			}
			if (!IsOnFloor() && animation.Animation != "attacking")
			{
				lock_anim = true;
				velocity.Y += gravity * (float)delta;
				if (velocity.Y < -10)
					animation.Play("jump");
				else if (velocity.Y > 10)
					animation.Play("fall");
				else 
					animation.Play("jump_to_fall");
			}
			else if (IsOnFloor())
				lock_anim = false;
				

			// Handle Jump.
			if (Input.IsActionJustPressed("jump") && IsOnFloor())
			{
				lock_anim = true;
				Jump();
			}
			if (Input.IsActionJustPressed("jump") && !IsOnFloor())
			{
				lock_anim = true;
				DoubleJump();
				doubleJump = true;
			}
			if (IsOnFloor())
			{
				doubleJump = false;
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
					TakeDamage(delta,(enemy)a);
					animation.Play("hurt");
					Velocity = velocity;
					MoveAndSlide();
				}
			}
		}
		else if (state == "damaged")
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
