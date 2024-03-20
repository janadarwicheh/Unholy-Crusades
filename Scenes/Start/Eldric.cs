using Godot;
using System;
using Skull.Scenes;
using Skull.Scenes.Map;

public partial class Eldric : Button
{
	private void Eldric_Pressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Map/world1.tscn");
		CurrentInfo.Update();
	}
}
