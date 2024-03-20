using System;
using System.Collections.Generic;
using Godot;
using Skull.Scenes.Entities;
using Skull.Scenes.Entities.Resources.Equipment;
using Skull.Scenes.Entities.Skills;
using Skull.Scenes.Entities.Skills.Player;
using Skull.Scenes.Entities.Stats;
using Armor = Skull.Scenes.Entities.Resources.Equipment.Armor;
using Resource = Skull.Scenes.Entities.Resources.Resource;

namespace Skull.Scenes.Player;

public class MeleeAttack : Skill
{
    public MeleeAttack(int damageAmount, int healAmount, int[] rng, int range, int cooldown) : base(damageAmount, healAmount, rng, range, cooldown)
    {
    }
}
public partial class Matt : Playeru
{
    public Dictionary<SkillType, Skill> MeleeSkills { get; set; }
    public Dictionary<SkillType, Skill> RangedSkills { get; set; }
    public string CurrentForm { get; set; }
    public Matt()
    {
        Parameters = new EntityComponent(new List<Resource>(), new Dictionary<StatType, Stat>(){{StatType.HitPoints, new HitPoints(50)},{StatType.Attack, new Attack(7)}, {StatType.NaturalArmor, new NaturalArmor(10)}, {StatType.Speed, new Speed(200)}}, new ObsidianHammer(),new LeatherJacket());
        CurrentForm = "Melee";
    }
    
    
    public override void _Ready()
    {
        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }
}