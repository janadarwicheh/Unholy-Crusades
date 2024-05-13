using System;
using Godot;
using Godot.Collections;
using Skull.Scenes.Entities.Skills;

namespace Skull.Scenes.Entities.Parameters;

public partial class Entity : CharacterBody2D
{
    public EntityComponent Parameters { get; set; }
    public Dictionary<string, State> states = new Dictionary<string, State>();

    
    public bool TakeDamage(Entity entity, int adder, float multiplier)
    {
        if (entity.Parameters.CurrentStats[StatType.Attack].Amount > 0)
        {
            int value = Convert.ToInt32((entity.Parameters.CurrentStats[StatType.Attack].Amount * multiplier + adder)* -1);
            Parameters.CurrentStats[StatType.HitPoints].Change(value);
            GD.Print("Took Damage: " + value + "\nNew Value: " + Parameters.CurrentStats[StatType.HitPoints].Amount);
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
        foreach (var node in this.GetChildren())
        {
            if (node is State state)
            {
                states[state.Name] = state;
            }
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }
}