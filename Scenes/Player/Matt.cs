﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Xml;
using Godot;
using Skull.Scenes.Entities;
using Skull.Scenes.Entities.Parameters;
using Skull.Scenes.Entities.Resources.Equipment;
using Skull.Scenes.Entities.Skills;
using Skull.Scenes.Entities.Skills.Player;
using Skull.Scenes.Entities.Stats;
using Armor = Skull.Scenes.Entities.Resources.Equipment.Armor;
using Entity = Skull.Scenes.Entities.Parameters.Entity;
using EntityHandler = Skull.Scenes.Entities.Parameters.EntityHandler;
using Resource = Skull.Scenes.Entities.Resources.Resource;

namespace Skull.Scenes.Player;

public partial class Matt : Playeru
{
    private Vector2 syncPos = new Vector2(0, 0);
    
    public string CurrentForm { get; set; }
    public PackedScene Projectile { get; set; }
    public override void _Ready()
    {
        base._Ready();
        Projectile = GD.Load<PackedScene>("res://Scenes/Entities/Projetctiles/MattBullet.tscn");
        GetNode<MultiplayerSynchronizer>("MultiplayerSynchronizer").SetMultiplayerAuthority(int.Parse(Name));
    }

    public Matt()
    {
        Parameters = new EntityHandler(50, 7, 10, 200, new ObsidianHammer(), new LeatherJacket());
        CurrentForm = "Melee";
        Skills.Add(PlayerSkill.Attack, new MattAttack(this, "Basic Attack", 10, null));
        Skills.Add(PlayerSkill.Special1, new MattShoot(this, "shoot", 3, null));
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
public class MattAttack: Skill
{
    public MattAttack(Entity user, string name, int damageAmount, int? cooldown) : base(user, name, damageAmount, cooldown)
    {
    }

    public override bool Launch()
    {
        return base.Launch();
    }
}

public class MattShoot : Skill
{
    private PackedScene Projectile { get; set; } =
        GD.Load<PackedScene>("res://Scenes/Entities/Projetctiles/MattBullet.tscn");
    public MattShoot(Entity user, string name, int damageAmount, int? cooldown) : base(user, name, damageAmount,
        cooldown)
        {
         }
    
    public override bool Launch()
    {
        Entities.Projetctiles.MattBullet Instance = (Entities.Projetctiles.MattBullet)Projectile.Instantiate();
        if (((Playeru)User).Sprite.FlipH)
        {
            Instance.Direction = 4.71239f;
        }
        else
        {
            Instance.Direction = 1.5708f;
        }
        Instance.SpawnPos = User.GlobalPosition;
        Instance.Parameters = new EntityHandler(1, User.Parameters.CurrentStats[StatType.Attack].Amount/2, 0, 0, null, null);
        User.GetParent().AddChild(Instance);
        return true;
    }
}

