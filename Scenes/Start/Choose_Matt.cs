using Godot;
using System;
using Skull.Scenes;
using Skull.Scenes.Player;

public partial class Choose_Matt : Button
{
    public void _on_pressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/Map/world1.tscn");
        CurrentInfo.player = new Matt();
    }
}
