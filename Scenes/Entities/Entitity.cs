using Godot;

namespace Skull.Scenes.Entities;

public partial class Entity : CharacterBody2D
{
    public EntityComponent Parameters { get; set; }
    public AnimatedSprite2D Animation;
    
}