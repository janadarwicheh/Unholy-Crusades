using System.Collections.Generic;
using Godot;
using Skull.Scenes.Entities;
using Skull.Scenes.Entities.Resources.Equipment;
using Skull.Scenes.Entities;
using Skull.Scenes.Entities.Parameters;
using Skull.Scenes.Entities.Resources.Equipment;
using Skull.Scenes.Entities.Skills;
using Skull.Scenes.Entities.Skills.Player;
using Skull.Scenes.Entities.Stats;
using EntityComponent = Skull.Scenes.Entities.Parameters.EntityComponent;
using Resource = Skull.Scenes.Entities.Resources.Resource;

namespace Skull.Scenes.Player;

public partial class Eldric : Playeru
{
	private Vector2 syncPos = new Vector2(0, 0);

	public Eldric()
	{
		Parameters = new EntityComponent(new List<Resource>(), new Dictionary<StatType, Stat>(){{StatType.HitPoints, new HitPoints(80)},{StatType.Attack, new Attack(10)}, {StatType.NaturalArmor, new NaturalArmor(8)}, {StatType.Speed, new Speed(90)}}, new BronzeWarHammer(),new IronChestplate());
	}
	public override void _Ready()
	{
		base._Ready();
		GetNode<MultiplayerSynchronizer>("MultiplayerSynchronizer").SetMultiplayerAuthority(int.Parse(Name));
	}

	public override void _PhysicsProcess(double delta)
	{
		if (GetNode<MultiplayerSynchronizer>("MultiplayerSynchronizer").GetMultiplayerAuthority() ==
		    Multiplayer.GetUniqueId())
		{
			syncPos = GlobalPosition;
		}
		else
		{
			GlobalPosition = GlobalPosition.Lerp(syncPos, .1f);
		}
		base._PhysicsProcess(delta);
	}
}
