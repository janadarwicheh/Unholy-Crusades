﻿
#nullable enable
using System.Collections.Generic;
using Godot;
using Skull.Scenes.Entities.Resources.Equipment;
using Skull.Scenes.Entities.Skills;
using Skull.Scenes.Entities.Stats;
using Resource = Skull.Scenes.Entities.Resources.Resource;

namespace Skull.Scenes.Entities.Parameters;

public class EntityComponent
{
    public Dictionary<StatType, Stat> BaseStats { get; set; }
    public Dictionary<StatType, Stat> BonusStats { get; set; }
    public Dictionary<StatType, Stat> CurrentStats { get; set; }
    public Weapon? CurrentWeapon { get; set; }
    public Armor? CurrentArmor { get; set; }
    public List<Resource> Inventory;
    public EntityComponent(List<Resource> inventory, Dictionary<StatType, Stat> baseStats, Weapon? currentWeapon, Armor? currentArmor)
    {
        CurrentStats = new Dictionary<StatType, Stat>() {{ StatType.Attack, new Attack(0) }, { StatType.HitPoints, new HitPoints(0) }, { StatType.NaturalArmor, new NaturalArmor(0) }, { StatType.Speed, new Speed(0)}};
        Inventory = inventory;
        BaseStats = baseStats;
        CurrentWeapon = currentWeapon;
        CurrentArmor = currentArmor;
        BonusStats = new Dictionary<StatType, Stat>() {{ StatType.Attack, new Attack(0) }, { StatType.HitPoints, new HitPoints(0) }, { StatType.NaturalArmor, new NaturalArmor(0) }, { StatType.Speed, new Speed(0)}};
        if (CurrentArmor != null)
        {
            foreach (var stat in CurrentArmor.GivenStats)
            {
                BonusStats[stat.Type].Amount += stat.Amount;
            }
        }
        if (CurrentWeapon != null)
        {
            foreach (var stat in CurrentWeapon.GivenStats)
            {
                BonusStats[stat.Type].Amount += stat.Amount;
            }
        }
        UpdateStats();
    }
    
     public void UpdateStats()
    {
        foreach (var stat in BaseStats)
        {
            CurrentStats[stat.Key].Amount = stat.Value.Amount + BonusStats[stat.Key].Amount;
        }
    }

     public void UnequipWeapon()
     {
         if (CurrentArmor == null)
             return;
         foreach (var stat in CurrentWeapon.GivenStats)
         {
             BonusStats[stat.Type].Amount -= stat.Amount;
         }

         Inventory.Add(CurrentWeapon);
     }
    public void EquipWeapon(Weapon weapon)
    {
        if (weapon.GetAbility())
        {
            UnequipWeapon();

            CurrentWeapon = weapon;
            foreach (var stat in weapon.GivenStats)
            {
                BonusStats[stat.Type].Amount += stat.Amount;
            }

            UpdateStats();
        }
        else
        {
            GD.Print("You do not have the ability to use this weapon");
        }
    }

    public void UnequipArmor()
    {
        if (CurrentArmor != null)
        {
            foreach (var stat in CurrentArmor.GivenStats)
            {
                BonusStats[stat.Type].Amount -= stat.Amount;
            }

            Inventory.Add(CurrentArmor);
        }
    }
    public void EquipArmor(Armor armor)
    {
        if(armor.Usability())
        {
            UnequipArmor();

            CurrentArmor = armor;
            foreach (var stat in armor.GivenStats)
            {
                BonusStats[stat.Type].Amount += stat.Amount;
            }

            UpdateStats();
        }
        else
        {
            GD.Print("You do not have the ability to use this armor");
        }
    }
    
    public void Get(Resource res)
    {
        Inventory.Add(res);
    }
}

public class EntityHandler : EntityComponent
{
    public EntityHandler(int hp, int attack, int armor, int speed, Weapon? currentWeapon, Armor? currentArmor) : base(new List<Resource>(), new Dictionary<StatType, Stat> ()
        {
            { StatType.HitPoints , new HitPoints(hp)},
            { StatType.Attack , new Attack(attack)},
            { StatType.NaturalArmor , new NaturalArmor(armor)},
            { StatType.Speed , new Speed(speed)}
        }, currentWeapon, currentArmor)
    {
    }
}