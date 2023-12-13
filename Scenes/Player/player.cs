using Godot;
using System;
using System.ComponentModel;

public partial class player : CharacterBody2D
{
	[Export]
	public float speed = 200.0f;
	
	AnimationTree animation_tree;
	Vector2 direction = Vector2.Zero;
	Sprite2D sprite;
	public Vector2 velocity = Vector2.Zero;
	CharacterStateMachine state_machine;
	bool was_air = false;
	

	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("Sprite2D");
		animation_tree = GetNode<AnimationTree>("AnimationTree");
		animation_tree.Active = true; 
		state_machine = GetNode<CharacterStateMachine>("CharacterStateMachine");
	}


	bool double_jump = false;
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	
	private void OnAnimationFinished()
	{
		animation_tree.Set("parameters/Move/blend_position", direction.X);
		side_update();
	}

	private void inbetween()
	{

	}

	private void fall()
	{

	}

	private void side_update()
	{
		if (direction.X < 0 )
			sprite.FlipH = true;
		else 
			if (direction.X > 0 )
				sprite.FlipH = false;
	}

	

	public override void _PhysicsProcess(double delta)
	{
		velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;
		else 
			{
				double_jump = false;
			}
		// Handle Jump.

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, speed);
		}
	
		OnAnimationFinished();
		Velocity = velocity;
		MoveAndSlide();
	}
}
