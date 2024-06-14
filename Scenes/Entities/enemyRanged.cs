using Godot;
using Skull.Scenes.Entities.Parameters;
using Skull.Scenes.Entities.Projetctiles;
using Skull.Scenes.Entities.Skills;
using Skull.Scenes.Entities.Stats;
using Skull.Scenes.Player;
using Skull.Scenes.Timers;
using EntityHandler = Skull.Scenes.Entities.Parameters.EntityHandler;
namespace Skull.Scenes.Entities;

public partial class enemyRanged : Entity
{
	Godot.Area2D area_right;
	Godot.Area2D area_left;
	bool MovementLock = false;

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
	public AnimationNodeStateMachinePlayback Anim;
	[Export] public Sprite2D Sprite2D;
	private PackedScene Projectile;
	public BasicCooldown ShootCooldown;

	private void Shoot()
	{
		var Instance = (MattBullet)Projectile.Instantiate();
		if (Sprite2D.FlipH)
		{
			Instance.Direction = 4.71239f;
		}
		else
		{
			Instance.Direction = 1.5708f;
		}
		
		Instance.SpawnPos = GlobalPosition;
		Instance.Speed = 5000;
		Instance.Parameters = new Parameters.EntityHandler(1, Parameters.CurrentStats[StatType.Attack].Amount / 2, 0, 0,
			null, null);
		GetParent().AddChild(Instance);
	}
	
	private void _on_area_2d_area_entered_right(Godot.Area2D area)
	{
		MovementLock = true;
		area_right.Monitoring = false;
		area_left.Monitoring = false;
		Anim.Travel("attack");
	}
	private void _on_area_2d_area_entered_left(Godot.Area2D area)
	{
		MovementLock = true;
		area_right.Monitoring = false;
		area_left.Monitoring = false;
		Anim.Travel("attack");
	}

	private void _on_animation_tree_animation_finished(string name)
	{
		MovementLock = false;
		area_right.Monitoring = false;
		area_left.Monitoring = false;
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
		Projectile = GD.Load("res://Scenes/Entities/Projetctiles/EnemyBullet.tscn") as PackedScene;
		AnimationTree = GetNode<AnimationTree>("AnimationTree");
		Anim = GetNode<AnimationTree>("AnimationTree").Get("parameters/playback").As<AnimationNodeStateMachinePlayback>();;
		_sprite2D = Sprite2D; 
		velocity = Vector2.Zero;
		velocity.X = speed;
		area_right = GetNode<Godot.Area2D>("Area2DRight");
		area_left = GetNode<Godot.Area2D>("Area2DLeft");
		Down[0] = GetNode<RayCast2D>("RayCast2DDownLeft");
		Down[1] = GetNode<RayCast2D>("RayCast2DDownRight");
		Side = GetNode<RayCast2D>("RayCast2DSide");
		Parameters = new Parameters.EntityHandler(20, 3, 1, 150, null, null);
		base.RedGlowCooldown = new BasicCooldown(0.2f);
		AddChild(RedGlowCooldown);

	}
	
	
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
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