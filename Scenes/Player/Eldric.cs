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
using Skull.Scenes.Timers;
using EntityComponent = Skull.Scenes.Entities.Parameters.EntityComponent;
using Resource = Skull.Scenes.Entities.Resources.Resource;
using EntityHandler = Skull.Scenes.Entities.Parameters.EntityHandler;

namespace Skull.Scenes.Player;

public partial class Eldric : Playeru
{
	private Vector2 syncPos = new Vector2(0, 0);

	public Eldric()
	{
		Parameters = new EntityHandler(80, 10, 8, 90, new BronzeWarHammer(), new IronChestplate());
		
		Skills.Add(PlayerSkill.Attack, new EldricAttack(this, "Basic Attack", 15, null));
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

public class EldricAttack: Skill
{
	public EldricAttack(Entity user, string name, int damageAmount, BasicCooldown? cooldown) : base(user, name, damageAmount, cooldown)
	{
	}

	public override bool Launch()
	{
		return base.Launch();
	}
}
