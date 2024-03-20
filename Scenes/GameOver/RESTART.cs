using Godot;
using System;using Skull.Scenes.Map;

public partial class RESTART : Control
{
    public void _restart_on_pressed()
    {
        GD.Print("restart scene");
        GetTree().ChangeSceneToFile("res://Scenes/Map/Start.tscn");
    }
	
}