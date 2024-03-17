using System.Collections.Generic;
using System.Security.AccessControl;
using Skull.Scenes.Entities.Stats;

namespace Skull.Scenes.Entities.Resources.Equipment;

public abstract class Weapon : Resource
{
    public List<Stat> GivenStats { get; set; }
    public Weapon(string name, ResourceType type, string description,List<Stat> givenStats) : base(name, type, description)
    {
        GivenStats = givenStats;
    }
}

public class ObsidianHammer : Weapon
{
    public ObsidianHammer() : base("Obsidian Hammer", ResourceType.Weapon, "A heavy hammer made of obsidian", new List<Stat>(){new Attack(10)})
    {
    }
}