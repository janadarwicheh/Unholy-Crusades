using System.Security.AccessControl;

namespace Skull.Scenes.Entities.Resources;

public abstract class Resource
{
    // Classe qui combine tous types d'objets collectables pour l'inventaire. 
    protected Resource(string name, ResourceType type, string description)
    {
        Name = name;
        Description = description;
        Type = type;
    }
    
    public string Name { get; }
    public ResourceType Type { get; }
    public string Description { get; }
    
    public virtual bool Get(EntityComponent entity)
    {
        if (GetAbility()) 
        {
            entity.Get(this);
            return true;
        }
        return false;
    }

    public virtual string GetInfo()
    {
        return $"Name: {Name}\nType: {Type}\n{Description}";
    }
    public virtual bool GetAbility()
    {
        return true;
    }

    public virtual bool Usability()
    {
        return true;
    }
}