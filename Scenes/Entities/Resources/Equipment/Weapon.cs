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
    public ObsidianHammer() : base("Obsidian mallet", ResourceType.Weapon, "A homemade light mallet of obsidian and wood. It was designed to be easy to carry and wield.", new List<Stat>(){new Attack(8), new Speed(10)})
    {
    }
}
public class BronzeWarHammer : Weapon
{
    public BronzeWarHammer() : base("Bronze WarHammer", ResourceType.Weapon, "A heavy human sized hammer made of high quality bronze. The Church's Symbol is cast on it's head.", new List<Stat>(){new Attack(15), new Speed(-5), new NaturalArmor(3)})
    {
    }
}