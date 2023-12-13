using Godot;
using System;
using System.Diagnostics;

public partial class enemy : CharacterBody2D
{
	Area2D area_right;
	Area2D area_left;
	bool animationlock;
	public AnimatedSprite2D animation;
	public Node parent1;
	public Enemies parent;
	public Playeru player;

	private void attack()
	{
		animation.Play("attack");
	}
	private void _on_area_2d_area_entered_right(Area2D area)
	{
		animation.FlipH = false;
		animationlock = true;
		attack();
		parent.halt();
	}
	private void _on_area_2d_area_entered_left(Area2D area)
	{
		animation.FlipH = true;
		animationlock = true;
		attack();
		parent.halt();
	}

	private void _on_animated_sprite_2d_animation_finished()
	{
		if (animation.Animation == "attack")
		{
			animationlock = false;
			parent.start();
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		area_right = GetNode<Area2D>("Area2DRight");
		area_left = GetNode<Area2D>("Area2DLeft");
		animation = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		parent1 = GetParent().GetParent().GetParent();
		GD.Print(parent1.GetPath());
		parent = (Enemies)parent1;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public override void _PhysicsProcess(double delta)
	{
		if (animation.FlipH)
		{
			area_right.Monitoring = false;
			area_left.Monitoring = true;
		}
		else
		{
			area_right.Monitoring = true;
			area_left.Monitoring = false;
		}
		if (!animationlock)
			animation.Play("default");
	}
}
