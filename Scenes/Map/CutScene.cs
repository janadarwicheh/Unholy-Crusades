using System;
using Godot;
using Skull.Scenes;
using Skull.Scenes.Entities.Skills;
using Skull.Scenes.Player;


public partial class CutScene : Node2D
{
	public Playeru Joueur;
	public PackedScene Tamer;
	public bool CharacterChosen = false;
	public override void _Ready()
	{
		Joueur = CurrentInfo.Player;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!CharacterChosen)
		{
			if (CurrentInfo.Player is Eldric)
			{
				Tamer = (PackedScene)GD.Load("res://Scenes/Player/Eldric.tscn");
				CharacterChosen = true;
			}
			else if (CurrentInfo.Player is Matt)
			{
				Tamer = (PackedScene)GD.Load("res://Scenes/Player/Matt.tscn");
				CharacterChosen = true;
			}
			if (CharacterChosen)
			{
				Joueur = (Playeru)Tamer.Instantiate();
				Camera2D cam = new Camera2D();
				cam.PositionSmoothingEnabled = true;
				cam.Zoom = new Vector2(0.125f, 0.125f);
				Joueur.AddChild(cam);
				Joueur.GlobalPosition = new Vector2(0,0);
				GetNode("Player/RemoteTransform2D").AddChild(Joueur);
				var a = (RemoteTransform2D)GetNode("Player/RemoteTransform2D");
				a.RemotePath = Joueur.GetPath();
				a.UpdateScale = false;
				CurrentInfo.Player = Joueur;
			}
		}

		if (Joueur.GlobalPosition.X >= 31000)
		{
			GetTree().ChangeSceneToFile("res://Scenes/Map/world1.tscn");
		}
		
	}
}
