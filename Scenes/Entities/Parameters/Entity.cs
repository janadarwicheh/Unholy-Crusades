using System;
using Godot;
using Godot.Collections;
using Skull.Scenes.Entities.Skills;
using Skull.Scenes.Timers;

namespace Skull.Scenes.Entities.Parameters;

public partial class Entity : CharacterBody2D
{
    public EntityComponent Parameters { get; set; }
    public Dictionary<string, State> states = new Dictionary<string, State>();
    private Sprite2D _sprite2D;
    BasicCooldown RedGlowCooldown { get; set; }
    public bool TakeDamage(Entity entity, int adder, float multiplier)
    {
        if (entity.Parameters.CurrentStats[StatType.Attack].Amount > 0)
        {
            int value = Convert.ToInt32((entity.Parameters.CurrentStats[StatType.Attack].Amount * multiplier + adder)* -1);
            Parameters.CurrentStats[StatType.HitPoints].Change(value);
            GD.Print("Took Damage: " + value + "\nNew Value: " + Parameters.CurrentStats[StatType.HitPoints].Amount);
            _sprite2D.Modulate = new Color(1, 0, 0);
            RedGlowCooldown.Create();
            if (Parameters.CurrentStats[StatType.HitPoints].Amount <= 0)
            {
                GD.Print("Dead");
                Die();
                return false;
            }
            return true;
        }
        return false;
    }
    
    public virtual void Die()
    {
        QueueFree();
    }
    
    public override void _Ready()
    {
        RedGlowCooldown = new BasicCooldown(0.2f);
        AddChild(RedGlowCooldown);
        _sprite2D = GetNode<Sprite2D>("Sprite2D");
        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
        if (RedGlowCooldown.IsStopped())
            _sprite2D.Modulate = new Color(1, 1, 1);
        base._PhysicsProcess(delta);
    }
}