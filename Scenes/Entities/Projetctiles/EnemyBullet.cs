using Godot;
using Skull.Scenes.Entities.Parameters;

namespace Skull.Scenes.Entities.Projetctiles;

public partial class EnemyBullet : Entity,  IProjectile
{
    [Export] public float Speed = 5000;
    public float Direction;

    public void AreaEntered()
    {
        QueueFree();
    }

    public Vector2 SpawnPos;

    public float SpawnRot;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GlobalPosition = SpawnPos;
        GlobalRotation = SpawnRot;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        Velocity = new Vector2(0, -Speed).Rotated(Direction);
        MoveAndSlide();
    }

    public void _on_timer_timeout()
    {
        QueueFree();
    }
}