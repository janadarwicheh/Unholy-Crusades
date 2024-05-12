using Godot;

namespace Skull.Scenes.Entities.Parameters;

public partial class State : Node
{
    public bool Enter()
    {
        return true;
    }

    public bool Exit()
    {
        return true;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }

}