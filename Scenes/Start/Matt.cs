using Godot;
using System;
using Skull.Scenes;

public partial class Matt : Button
{
    private void Matt_Pressed()
    {
        CurrentInfo.player = new Skull.Scenes.Player.Matt();
        GetTree().ChangeSceneToFile("res://Scenes/Map/world1.tscn");
    }
}
