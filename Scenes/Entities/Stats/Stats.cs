namespace Skull.Scenes.Entities.Stats;

public class Stats
{
    protected int Amount ;
    
    public Stats(int amount)
    {
        Amount = amount;
    }
  

    public void Change(int amount)
    {
        
    }
}

public class HitPoints : Stats
{
    public HitPoints(int amount) : base(amount)
    {
        Amount = amount;
    }
    public void Death()
    {
        
    }
}

public class Attack : Stats 
{
    public Attack(int amount) : base(amount)
    {
        Amount = amount;
    }
}

public class Armor : Stats
{
    public Armor(int amount) : base(amount)
    {
        Amount = amount;
    }
}

public class Speed : Stats
{
    public Speed(int amount) : base(amount)
    {
        Amount = amount;
    }
}

public abstract class Capacities
{
    public Capacities(int damageAmount, int healAmount, int[] rng, int range, int cooldown)
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
    
    protected abstract void launch();

}



