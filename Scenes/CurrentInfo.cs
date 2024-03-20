using Godot;
using Skull.Scenes.Map;

namespace Skull.Scenes;

public partial class CurrentInfo : Node
{
    public static World CurrentScene;
    public static Playeru player;
    public static float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle() * 20;
    public override void _Ready()
    {
        CurrentScene = (World)GetTree().CurrentScene;
    }

    public void Update()
    {
        CurrentScene = (World)GetTree().CurrentScene;
    }
}