using System;
using System.Collections.Generic;
using System.Xml;
using Godot;
using Skull.Scenes.Entities;
using Skull.Scenes.Entities.Parameters;
using Skull.Scenes.Entities.Resources.Equipment;
using Skull.Scenes.Entities.Skills;
using Skull.Scenes.Entities.Skills.Player;
using Skull.Scenes.Entities.Stats;
using Armor = Skull.Scenes.Entities.Resources.Equipment.Armor;
using EntityHandler = Skull.Scenes.Entities.Parameters.EntityHandler;
using Resource = Skull.Scenes.Entities.Resources.Resource;

namespace Skull.Scenes.Player;

public partial class Matt : Playeru
{
    public string CurrentForm { get; set; }
    public Matt()
    {
        Parameters = new EntityHandler(50, 7, 10, 200, new ObsidianHammer(), new LeatherJacket());
        CurrentForm = "Melee";
        Skills.Add();
    }
    
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }
}
public class MattAttack: Skill
{

}

