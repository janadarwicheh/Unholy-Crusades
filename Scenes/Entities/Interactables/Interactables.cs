using System.Collections.Generic;
using Skull.Scenes.Entities.Resources;

namespace Skull.Scenes.Entities.Interactables;

public abstract class Interactables
{
    public abstract void Interact();
    public Dictionary<Resource, bool> Givables;
    

    protected Interactables(Dictionary<Resource, bool> givables)
    {
        Givables = givables;
    }
}