namespace Skull.Scenes.Entities;

public partial class Dummy : Parameters.Entity
{
    public Dummy()
    {
        Parameters = new Parameters.EntityHandler(30000, 5, 5, 0, null, null);
    }
}