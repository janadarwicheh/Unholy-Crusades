using Godot;
using Skull.Scenes.Player;

namespace Skull.Scenes.Start;

public partial class Choose_Eldric : Button
{
    public void _on_pressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/Map/CutScene.tscn");
        CurrentInfo.Player = new Eldric();
    }
}