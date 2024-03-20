using Godot;
using Godot.Collections;
using System.Collections.Generic;
namespace Skull.Scenes.Entities;

public class HitboxHandler
{
    public CharacterBody2D User;
    
    public System.Collections.Generic.Dictionary<Area2D, List<CollisionShape2D>> CurrentHitBox { get; set; }
    
    public HitboxHandler(CharacterBody2D user)
    {
        User = user;


        void test()
        {
            CurrentHitBox = new System.Collections.Generic.Dictionary<Area2D, List<CollisionShape2D>>();
        }
    }
    
    
    
    
    
    
}