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
		Joueur = CurrentInfo.player;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!CharacterChosen)
		{
			if (CurrentInfo.player is Eldric)
			{
				Tamer = (PackedScene)GD.Load("res://Scenes/Player/Eldric.tscn");
				CharacterChosen = true;
			}
			else if (CurrentInfo.player is Matt)
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
				CurrentInfo.player = Joueur;
			}
		}
		else
		{
		}

		if (Joueur.GlobalPosition.Y >= 21480)
		{
			Joueur.GlobalPosition = new Vector2(2224, 6062);
		}
	}
}
