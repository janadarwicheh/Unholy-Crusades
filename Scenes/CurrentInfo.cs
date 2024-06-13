using Godot;
using Skull.Scenes.Map;

namespace Skull.Scenes;

public partial class CurrentInfo : Node
{
    public static World CurrentScene;
    public static Playeru Player;
    public static float Gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle() * 40;
    public static bool FrameCounter = false;
    public override void _Ready()
    {
        Player = new Playeru();
        CurrentScene = (World)GetTree().CurrentScene;
    }

    public void Update()
    {
        CurrentScene = (World)GetTree().CurrentScene;
    }

    public override void _Process(double delta)
    {
        FrameCounter = !FrameCounter;
    }
}