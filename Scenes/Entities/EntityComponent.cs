
using System.Collections.Generic;
using Godot;
using Skull.Scenes.Entities.Stats;
using Resource = Skull.Scenes.Entities.e.Resources.Resource;

namespace Skull.Scenes.Entities;

public class EntityComponent
{
    public List<Stats.Stats> StatsList;
    public List<Resource> Inventory;
    public EntityComponent(List<Stats.Stats> statsList, List<Resource> inventory)
    {
        StatsList = statsList;
        Inventory = inventory;
    }

    public void Get(Resource res)
    {
        Inventory.Add(res);
    }
}