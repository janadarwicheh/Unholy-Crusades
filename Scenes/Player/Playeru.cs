#nullable enable
using Skull.Scenes.Entities.Skills.Player;
using Godot;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Godot.Collections;
using Skull.Scenes;
using Skull.Scenes.Entities;
using Skull.Scenes.Entities.Parameters;
using Skull.Scenes.Entities.Skills;
using Entity = Skull.Scenes.Entities.Parameters.Entity;
using Skull.Scenes.Entities.Skills.Player;

public partial class Playeru : Entity
{
	protected AnimationTree AnimationTree;
	public Sprite2D Sprite;
	private float _gravity;
	protected float JumpVelocity = -11000;
	public bool jumped = false;
	protected float Speed;
	public Skill? CastingSkill = null;
	float run_mult = 1.5f;
	private Vector2 currentdir;
	private Area2D actionableFinder;
	public Area2D Attack;
	public AnimationNodeStateMachinePlayback AnimationTreePlayback;
	public AnimationNodeStateMachinePlayback AnimationTreePlaybackSkill;
	public System.Collections.Generic.Dictionary<PlayerSkill, Skill> Skills = new System.Collections.Generic.Dictionary<PlayerSkill, Skill>();
	public Area2D Hurtbox;
	
	
	public override void _Ready()
	{
		base._Ready();
		Sprite = GetNode<Sprite2D>("Sprite2D");
		AnimationTree = GetNode<AnimationTree>("AnimationTree");
		AnimationTree.Active = true;
		_gravity = CurrentInfo.Gravity;
		actionableFinder = GetNode<Area2D>("Direction/ActionableFinder");
		Attack = GetNode<Area2D>("Attack");
		AnimationTreePlayback = GetNode<AnimationTree>("AnimationTree").Get("parameters/playback").As<AnimationNodeStateMachinePlayback>();
		AnimationTreePlaybackSkill = GetNode<AnimationTree>("AnimationTree").Get("parameters/SkillUsed/playback").As<AnimationNodeStateMachinePlayback>();
		Hurtbox = GetNode<Area2D>("Hurtbox");

	}

	public override void Die()
	{
		GetTree().ChangeSceneToFile("res://Scenes/GameOver/game_over_screen.tscn");
	}


	public void UpdateAnimationParamaters()
	{
		if (IsOnFloor())
		{
			AnimationTree.Set("parameters/conditions/Airborne", false);
			AnimationTree.Set("parameters/conditions/Grounded", true);
			AnimationTree.Set("parameters/conditions/Jump", false);
			if (CastingSkill!=null)
			{
				AnimationTreePlayback.Travel("SkillUsed");
			}
			else
			{
				AnimationTree.Set("parameters/conditions/SkillUsed", false);
				if (Velocity.X > 0)
				{
					AnimationTree.Set("parameters/conditions/Idle", false);
					AnimationTree.Set("parameters/conditions/IsMoving", true);
					Sprite.FlipH = false;
					Attack.RotationDegrees = 0;
				}
				else if (Velocity.X < 0)
				{
					AnimationTree.Set("parameters/conditions/Idle", false);
					AnimationTree.Set("parameters/conditions/IsMoving", true);
					Sprite.FlipH = true;
					Attack.RotationDegrees = 180;
				}
				else
				{
					AnimationTree.Set("parameters/conditions/Idle", true);
					AnimationTree.Set("parameters/conditions/IsMoving", false);
				}
			}
		}
		else
		{
				if (CastingSkill!=null)
				{
					AnimationTreePlayback.Travel("SkillUsed");
					AnimationTree.Set("parameters/conditions/Idle", false);
					AnimationTree.Set("parameters/conditions/IsMoving", false);
				}
				else
				{
					AnimationTree.Set("parameters/conditions/SkillUsed", false);
				}
				if (jumped)
				{
					AnimationTree.Set("parameters/conditions/Jump", true);
					jumped = false;
				}
				
				else
				{
					AnimationTree.Set("parameters/conditions/Airborne", true);
				}
				AnimationTree.Set("parameters/conditions/Grounded", false);
				AnimationTree.Set("parameters/conditions/Idle", false);
				AnimationTree.Set("parameters/conditions/IsMoving", false);
		}

	}

	protected void SkillUsed(PlayerSkill sk)
	{
		if (CastingSkill == null)
		{
			CastingSkill = Skills[sk];
			Skills[sk].Launch();
		}
		
	}

	protected void Reload()
	{
		// This function will reload every parameter with updated stats and values. Must be called after every change in those values. 
		_gravity = CurrentInfo.Gravity;
		CurrentInfo.Player = this;
	}
	
	public void on_f_pressed()
	{
		Array<Area2D> actionables = actionableFinder.GetOverlappingAreas();
		if (actionables.Count > 0)
		{
			(actionables[0] as actionable).Action();
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		AnimationTree.Set("parameters/conditions/SkillUsed", false);	
		CastingSkill = null;
		Vector2 velocity = Velocity;
		if (Input.IsActionJustPressed("ui_accepted"))
		{
			on_f_pressed();
		}
		if (Input.IsActionJustPressed("attack"))
		{
			SkillUsed(PlayerSkill.Attack);
		}
		else if (Input.IsActionJustPressed("special1"))
		{
			SkillUsed(PlayerSkill.Special1);
		}

		if (Input.IsActionPressed("shift"))
		{
			Speed = (Parameters.CurrentStats[StatType.Speed].Amount * 3 + 2000)*run_mult;
		}
		else
		{
			Speed = Parameters.CurrentStats[StatType.Speed].Amount * 3 + 2000;
		}
		if (!IsOnFloor())
		{
			velocity.Y += _gravity * (float)delta;
		}
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
			jumped = true;
		}
		Vector2 direction = Input.GetVector("move_left", "move_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = Mathf.MoveToward(Velocity.X, Speed * direction.X, Speed/8);
		}
		else 
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed/8);
		}
		Velocity = velocity;
		UpdateAnimationParamaters();
		MoveAndSlide();
	}
	
}
