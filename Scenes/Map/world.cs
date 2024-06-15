using System;
using Godot;
using Skull.Scenes.Entities.Skills;
using Skull.Scenes.Player;

namespace Skull.Scenes.Map;

public partial class World : Node
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
				cam.Zoom = new Vector2(0.150f, 0.150f);
				Joueur.AddChild(cam);
				Joueur.GlobalPosition = new Vector2(0,0);
				GetNode("Player/RemoteTransform2D").AddChild(Joueur);
				var a = (RemoteTransform2D)GetNode("Player/RemoteTransform2D");
				a.RemotePath = Joueur.GetPath();
				a.UpdateScale = false;
				CurrentInfo.Player = Joueur;
			}
		}

		if (Joueur.GlobalPosition.Y >= 32000)
		{
			GetTree().ChangeSceneToFile("res://Scenes/GameOver/game_over_screen.tscn");
		}
		
	}
}
