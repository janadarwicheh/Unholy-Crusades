using System;
using System.Threading.Tasks;

namespace Skull.Scenes.Entities.Skills;

public abstract class Skill
{
    public Skill(int damageAmount, int healAmount, int[] rng, int range, int cooldown)
    {
        DamageAmount = damageAmount;
        HealAmount = healAmount;
        Rng = rng;
        Range = range;
        Cooldown = cooldown;
    }
    
    protected int DamageAmount { get; set; }
    
    protected int HealAmount { get; set; } 
    
    protected int[] Rng { get; set; }
    
    protected int  Range { get;  set  ; }
    
    protected int Cooldown { get; set; }

    public bool CanLaunch {get; set;} = true;

    public void StartCooldown()
    {
        if (CanLaunch)
        {
            CanLaunch = false;
            Task.Delay(Cooldown).ContinueWith(t => CanLaunch = true);
        }
        else
        {
            Console.WriteLine("Skill is on cooldown");
        }
    }

    protected virtual void Launch()
    {
        StartCooldown();
    }

}