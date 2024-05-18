using System;
using System.Threading.Tasks;
using Godot;

namespace Skull.Scenes.Entities.Skills;

public abstract class Skill
{
    public Skill(Parameters.Entity user, string name, int damageAmount, int? cooldown)
    {
        User = user;
        Name = name;
        DamageAmount = damageAmount;
        Cooldown = cooldown;
        CanLaunch = true;
    }
    // IMPORTANT: FAIRE CORRESPONDRE NOM DE LA CAPACITÉ AVEC LE NOM DE L'ANIMATION ET DE L'AREA2D POUR LA HITBOX
    public string Name { get; set; }
    
    public Parameters.Entity User { get; set; }
    protected int DamageAmount { get; set; }
    
    protected int? Cooldown { get; set; }
    private bool CanLaunch { get; set; }

    public void StartCooldown()
    {
        
        if (Cooldown != null)
        {
            CanLaunch = false;
            Task.Delay((int)Cooldown!).ContinueWith(t => CanLaunch = true);
        }
        
    }

    public virtual bool Launch()
    {
        if (CanLaunch)
        {
            Console.WriteLine("Skill Launched!");
            return true;
        }
        Console.WriteLine("Skill is on cooldown");
        return false;
    }

}