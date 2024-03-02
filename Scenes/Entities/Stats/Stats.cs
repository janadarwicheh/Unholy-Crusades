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

public class HP : Stats
{
    public HP(int amount) : base(amount)
    {
        Amount = amount;
    }
    public void Death()
    {
        
    }
}

public class Hit_Points : Stats 
{
    public Hit_Points(int amount) : base(amount)
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
        Damage_Amount = damageAmount;
        Heal_Amount = healAmount;
        RNG = rng;
        Range = range;
        Cooldown = cooldown;
    }
    
    protected int Damage_Amount { get; set; }
    
    protected int Heal_Amount { get; set; } 
    
    protected int[] RNG { get; set; }
    
    protected int  Range { get;  set  ; }
    
    protected int Cooldown { get; set; }
    
    protected abstract void launch();

}



