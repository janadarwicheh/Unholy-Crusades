using System.Collections.Generic;
using Godot;

namespace Skull.Scenes.Entities;

public class HitboxHandler
{
    public CharacterBody2D User;
    public Dictionary<Area2D, List<CollisionShape2D>> Hitboxes;
    public HitboxHandler(CharacterBody2D user)
    {
        User = user;
        Hitboxes = new Dictionary<Area2D, List<CollisionShape2D>>();
        var a = user.GetChildren();
        foreach (var node in a)
        {
            if (node is Area2D area)
            {
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
}

public class HitboxReader
{
    public Dictionary<Area2D,Dictionary<int, int>> Reader { get; }
    
}

public class HitboxWriter
{
    public Dictionary<Area2D, List<CollisionShape2D>> Hitboxes;
}