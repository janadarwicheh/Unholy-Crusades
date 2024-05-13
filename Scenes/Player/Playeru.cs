#nullable enable
using Skull.Scenes.Entities.Skills.Player;
using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Skull.Scenes;
using Skull.Scenes.Entities;
using Skull.Scenes.Entities.Parameters;
using Skull.Scenes.Entities.Skills;
using Entity = Skull.Scenes.Entities.Parameters.Entity;
using Skull.Scenes.Entities.Skills.Player;

public partial class Playeru : Entity
{
	protected AnimationTree AnimationTree;
	protected Sprite2D Sprite;
	private float _gravity;
	protected float JumpVelocity = -6000;
	public bool jumped = false;
	protected float Speed;
	public Skill? CastingSkill = null;
	public Dictionary<PlayerSkill, Skill> Skills = new Dictionary<PlayerSkill, Skill>();
	public void UpdateAnimationParamaters()
	{
		if (IsOnFloor())
		{
			AnimationTree.Set("parameters/conditions/Airborne", false);
			AnimationTree.Set("parameters/conditions/Grounded", true);
			AnimationTree.Set("parameters/conditions/Jump", false);
			if (CastingSkill!=null)
			{
				AnimationTree.Set("parameters/conditions/SkillUsed", true);
			}
			else
			{
				AnimationTree.Set("parameters/conditions/SkillUsed", false);
				if (Velocity.X > 0)
				{
					AnimationTree.Set("parameters/conditions/Idle", false);
					AnimationTree.Set("parameters/conditions/IsMoving", true);
					Sprite.FlipH = false;
				}
				else if (Velocity.X < 0)
				{
					AnimationTree.Set("parameters/conditions/Idle", false);
					AnimationTree.Set("parameters/conditions/IsMoving", true);
					Sprite.FlipH = true;
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

	protected void SkillUsed()
	{
		if (CastingSkill != null)
		{
			if (Input.IsActionJustPressed("attack"))
			{
				CastingSkill = Skills[PlayerSkill.Attack];
			}
			else if (Input.IsActionJustPressed("special1"))
			{
				CastingSkill = Skills[PlayerSkill.Special1];
			}
			else if (Input.IsActionJustPressed("special2"))
			{
				CastingSkill = Skills[PlayerSkill.Special2];
			}
			else if (Input.IsActionJustPressed("ult"))
			{
				CastingSkill = Skills[PlayerSkill.Ultimate];
			}

			CastingSkill.Launch();
		}
	}
	public override void _Ready()
	{
		Sprite = GetNode<Sprite2D>("Sprite2D");
		AnimationTree = GetNode<AnimationTree>("AnimationTree");
		AnimationTree.Active = true;
		_gravity = CurrentInfo.Gravity;
	}

	protected void Reload()
	{
		// This function will reload every parameter with updated stats and values. Must be called after every change in those values. 
		_gravity = CurrentInfo.Gravity;
		CurrentInfo.Player = this;
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		float run_mult = 1.5f;
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
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}
		Velocity = velocity;
		UpdateAnimationParamaters();
		MoveAndSlide();
	}
	
}
