using System.Security.AccessControl;

namespace Skull.Scenes.Entities.e.Resources;

public abstract class Resource
{
    protected Resource(string name, ResourceType type, string description, bool getAbility)
    {
        Name = name;
        Description = description;
        GetAbility = getAbility;
        Type = type;
    }
    
    public string Name { get; }
    public ResourceType Type { get; }
    public bool GetAbility { get; set; }
    public string Description { get; }
    
    public virtual bool Get(EntityComponent entity, bool newGetAbility)
    {
        if (GetAbility) 
        {
            entity.Get(this);
            GetAbility = newGetAbility;
            return true;
        }
        return false;
    }

    public virtual string GetInfo()
    {
        return $"Name: {Name}\nType: {Type}\n{Description}";
    }
}