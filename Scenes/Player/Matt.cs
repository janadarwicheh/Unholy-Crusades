using System.Collections.Generic;
using Skull.Scenes.Entities;
using Skull.Scenes.Entities.Resources;
using Skull.Scenes.Entities.Resources.Equipment;
using Skull.Scenes.Entities.Skills;
using Skull.Scenes.Entities.Skills.Player;
using Skull.Scenes.Entities.Stats;
using Armor = Skull.Scenes.Entities.Resources.Equipment.Armor;

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
    public EntityComponent EC { get; set; }
    public Matt()
    {
        EC = new EntityComponent(new List<Resource>(), new Dictionary<StatType, Stat>(){{StatType.HitPoints, new HitPoints(150)},{StatType.Attack, new Attack(10)}, {StatType.NaturalArmor, new NaturalArmor(8)}, {StatType.Speed, new Speed(95)}}, null,null);
        CurrentForm = "Melee";
        // MeleeSkills = new Dictionary<SkillType, Skill>(){{Skills.Slash, "Slash"}, {Skills.Bash, "Bash"}, {Skills.Heal, "Heal"}};
        // RangedSkills = rangedSkills;
    }
    public override void _Ready()
    {
        base._Ready();
    }
    
}