using System.Collections.Generic;
using System.Security.AccessControl;
using Skull.Scenes.Entities.Resources;
using Skull.Scenes.Entities.Skills;
using ResourceType = Skull.Scenes.Entities.Resources.ResourceType;
using Skull.Scenes.Entities.Stats;

namespace Skull.Scenes.Entities.Resources.Equipment;

public abstract class Armor : Resource
{
    public List<Stat> GivenStats { get; set; }
    protected Armor(string name, ResourceType type, string description, List<Stat> givenStats) : base(name, type , description)
    {
        GivenStats = givenStats;
    }
}

public class LeatherJacket : Armor
{
    public LeatherJacket() : base("Leather Jacket", ResourceType.Armor, "A jacket made of a demon's strong skin. Leaving the smell aside, it's unexpectedly comfy.", new List<Stat> {new NaturalArmor(10)})
    {
    }
}

public class IronChestplate : Armor
{
    public IronChestplate() : base("Iron Chestplate", ResourceType.Armor, "A chestplate made of a blessed iron, cast in the shape of a cross.", new List<Stat> {new NaturalArmor(15), new Speed(-5)})
    {
    }
}