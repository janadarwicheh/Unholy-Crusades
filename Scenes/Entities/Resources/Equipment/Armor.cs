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
    protected Armor(string name, ResourceType type, string description, List<Stat> givenStats, List<Stat> givenStats1) : base(name, type , description)
    {
        GivenStats = givenStats1;
    }
}