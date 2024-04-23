using System.Collections.Generic;
using Godot;
using Skull.Scenes.Entities;
using Skull.Scenes.Entities.Resources.Equipment;
using Skull.Scenes.Entities;
using Skull.Scenes.Entities.Resources.Equipment;
using Skull.Scenes.Entities.Skills;
using Skull.Scenes.Entities.Skills.Player;
using Skull.Scenes.Entities.Stats;
using Resource = Skull.Scenes.Entities.Resources.Resource;

namespace Skull.Scenes.Player;

public partial class Eldric : Playeru
{
	public Dictionary<SkillType, Skill> MeleeSkills { get; set; }
	public Dictionary<SkillType, Skill> RangedSkills { get; set; }
	public Eldric()
	{
		Parameters = new EntityComponent(new List<Resource>(), new Dictionary<StatType, Stat>(){{StatType.HitPoints, new HitPoints(80)},{StatType.Attack, new Attack(10)}, {StatType.NaturalArmor, new NaturalArmor(8)}, {StatType.Speed, new Speed(90)}}, new BronzeWarHammer(),new IronChestplate());
	}
}
