using Godot;

namespace Skull.Scenes.Entities;

public partial class Enemy : Parameters.Entity
{
	// Called when the node enters the scene tree for the first time.
	Area2D area_right;
	Area2D area_left;
	bool MovementLock = false;
	public Sprite2D Sprite2D;
	public AnimationTree AnimationTree;
	RayCast2D[] Down = new RayCast2D[2];
	RayCast2D Side;
	[Export]
	public int moveDir = 1;
	[Export]
	public int motionRange;
	[Export]
	public float speed = 400.0f;
	Vector2 velocity;
	public float gravity = CurrentInfo.Gravity;
	
	private void _on_area_2d_area_entered_right(Area2D area)
	{
		MovementLock = true;
		AnimationTree.Set("parameters/conditions/InRange", true);
		AnimationTree.Set("parameters/conditions/InRange", false);
	}
	private void _on_area_2d_area_entered_left(Area2D area)
	{
		MovementLock = true;
		AnimationTree.Set("parameters/conditions/InRange", true);
		AnimationTree.Set("parameters/conditions/InRange", false);
	}
	
	private bool ShouldTurn()
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
		AnimationTree = GetNode<AnimationTree>("AnimationTree");
		Sprite2D = GetNode<Sprite2D>("Sprite2D");
		velocity = Vector2.Zero;
		velocity.X = speed;
		area_right = GetNode<Area2D>("Area2DRight");
		area_left = GetNode<Area2D>("Area2DLeft");
		Down[0] = GetNode<RayCast2D>("RayCast2DDownLeft");
		Down[1] = GetNode<RayCast2D>("RayCast2DDownRight");
		Side = GetNode<RayCast2D>("RayCast2DSide");
		Parameters = new Parameters.EntityHandler(30, 4, 2, 250, null, null);
	}
	
	
	public override void _PhysicsProcess(double delta)
	{
		velocity = Velocity;
		if (ShouldTurn())
			moveDir *= -1;
		if (moveDir == -1)
		{
			Sprite2D.FlipH = true;
		}
		else
		{
			Sprite2D.FlipH = false;
		}
		if (Sprite2D.FlipH)
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
		if (!MovementLock)
		{
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