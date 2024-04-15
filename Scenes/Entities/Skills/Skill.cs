using System;
using System.Threading.Tasks;
using Godot;

namespace Skull.Scenes.Entities.Skills;

public abstract class Skill
{
    public Skill(Entity user, string name, int damageAmount, int healAmount, int[] rng, int range, int cooldown, Animation animation)
    {
        User = user;
        Name = name;
        DamageAmount = damageAmount;
        HealAmount = healAmount;
        Rng = rng;
        Range = range;
        Cooldown = cooldown;
        Animation = animation;
        HitBox = new HitBox(this);
    }
    // IMPORTANT: FAIRE CORRESPONDRE NOM DE LA CAPACITÉ AVEC LE NOM DE L'ANIMATION ET DE L'AREA2D POUR LA HITBOX
    public string Name { get; set; }
    
    public Entity User { get; set; }
    public Animation Animation { get; set; }
    protected int DamageAmount { get; set; }
    
    protected int HealAmount { get; set; } 
    
    protected int[] Rng { get; set; }
    
    protected int  Range { get;  set  ; }
    
    protected int Cooldown { get; set; }

    public HitBox HitBox { get; set; }

    public bool CanLaunch {get; set;} = true;

    public void StartCooldown()
    {
        Task.Delay(Cooldown).ContinueWith(t => CanLaunch = true);
    }

    public virtual bool Launch()
    {
        if (CanLaunch)
        {
            CanLaunch = false;
            StartCooldown();
            return true;
        }
        Console.WriteLine("Skill is on cooldown");
        return false;
    }

}