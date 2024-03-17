﻿using Godot;
using Skull.Scenes.Entities.Skills;

namespace Skull.Scenes.Entities.Stats;

public abstract class Stat
{
    public int Amount ;
    public StatType Type;
    
    public Stat(int amount)
    {
        Amount = amount;
    }
  

    public void Change(int amount)
    {
        
    }
}

public class HitPoints : Stat
{
    public HitPoints(int amount) : base(amount)
    {
        Amount = amount; 
        Type = StatType.HitPoints;
    }
    public void Death()
    {
        
    }
}

public class Attack : Stat
{
    public Attack(int amount) : base(amount)
    {
        Amount = amount;
        Type = StatType.Attack;
    }
}

public class NaturalArmor : Stat
{
    public NaturalArmor(int amount) : base(amount)
    {
        Amount = amount;
        Type = StatType.NaturalArmor;
    }
}

public class Speed : Stat
{
    public Speed(int amount) : base(amount)
    {
        Amount = amount;
        Type = StatType.Speed;
    }
}