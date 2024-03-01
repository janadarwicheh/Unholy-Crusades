namespace Skull.Scenes.Entities.Stats;

public class Stats
{
    protected int amount ;

    public void Change(int amount)
    {
        
    }
}

public class HP : Stats
{
    public void Death()
    {
        
    }
}

public class Hit_Points : Stats 
{

}

public class Armor : Stats
{
    
}

public class Speed : Stats
{
    
}

public abstract class Capacities
{
    
    protected int Damage_Amount { get; set; }
    
    protected int Heal_Amount { get; set; } 
    
    protected int[] rng { get; set; }
    
    protected int  range { get;  set  ; }
    
    protected int cooldown { get; set; }
    
    protected abstract void launch();

}


public abstract class Spells : Capacities 
{
    
}
