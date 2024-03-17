using Godot;

namespace Skull.Scenes.Entities;

public class HitboxHandler
{
    public CharacterBody2D User;
    public HitboxHandler(CharacterBody2D user)
    {
        User = user;
    }
}