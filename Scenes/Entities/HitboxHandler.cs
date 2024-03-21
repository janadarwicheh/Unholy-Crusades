using System.Collections.Generic;
using Godot;

namespace Skull.Scenes.Entities;

using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;
public class Areas
{
    public System.Collections.Generic.Dictionary<string, Area2D> Animations;
}

public class HitboxHandler
{
    public CharacterBody2D User;
    public System.Collections.Generic.Dictionary<Area2D, List<CollisionShape2D>> Hitboxes;
    public HitboxReader Reader;
    public Areas Areas { get; }
    public object this[Area2D i]
    {
        get { return Hitboxes[i]; }
        set { Hitboxes[i] = (List<CollisionShape2D>)value; }
    }
    public HitboxHandler(CharacterBody2D user)
    {
        User = user;
        Reader = new HitboxReader();
        Hitboxes = new System.Collections.Generic.Dictionary<Area2D, List<CollisionShape2D>>();
        var a = user.GetChildren(); 
        Areas = new Areas();
        foreach (var node in a)
        {
            if (node is Area2D area)
            {
                Areas.Animations.Add(area.Name, area);
                Reader.Add(area, new System.Collections.Generic.Dictionary<int, int>());
                var z = area.GetChildren();
                List<CollisionShape2D> w = new List<CollisionShape2D>();
                foreach (var l in z)
                {
                    if (l is CollisionShape2D lol)
                    {
                        w.Add(lol);
                    }
                }
                Hitboxes.Add(area, w);
            }
        }
        GD.Print(Hitboxes.Count);
    }

    public bool ChangeReader(Area2D area, System.Collections.Generic.Dictionary<int, int> dico)
    {
        return Reader.Add(area, dico);
    }

    public void Update(string str)
    {
        
    }
}

public class HitboxReader
{
    public System.Collections.Generic.Dictionary<Area2D, System.Collections.Generic.Dictionary<int, int>> Reader { get; }
    public object this[Area2D i]
    {
        get { return Reader[i]; }
        set { Reader[i] = (System.Collections.Generic.Dictionary<int, int>)value; }
    }

    public bool Add(Area2D area, System.Collections.Generic.Dictionary<int, int> dico)
    {
        if (!Reader.Keys.Contains(area))
        {
            Reader.Add(area, dico);
            return true;
        }

        return false;
    }

}