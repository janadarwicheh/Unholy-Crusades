using System;
using Godot;
using Skull.Scenes.Entities.Skills;

namespace Skull.Scenes.Map;

public partial class World : Node
{
	private Playeru _joueur;
	private PackedScene tamer;
	private bool CharacterChosen = false;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!CharacterChosen)
		{
			if (Input.IsActionJustPressed("choose_eldric"))
			{
				tamer = (PackedScene)GD.Load("res://Scenes/Player/Eldric.tscn");
				CharacterChosen = true;
			}
			else if (Input.IsActionJustPressed("choose_matt"))
			{
				tamer = (PackedScene)GD.Load("res://Scenes/Player/Matt.tscn");
				CharacterChosen = true;
			}
			if (CharacterChosen)
			{
				_joueur = (Playeru)tamer.Instantiate();
				Camera2D cam = new Camera2D();
				cam.PositionSmoothingEnabled = true;
				cam.Zoom = new Vector2(0.125f, 0.125f);
				_joueur.AddChild(cam);
				_joueur.GlobalPosition = new Vector2(0,0);
				GetNode("Player/RemoteTransform2D").AddChild(_joueur);
				var a = (RemoteTransform2D)GetNode("Player/RemoteTransform2D");
				a.RemotePath = _joueur.GetPath();
				a.UpdateScale = false;
				CurrentInfo.player = _joueur;
			}
		}
		else
		{
			GD.Print(_joueur.GlobalPosition.X);
			GD.Print(_joueur.GlobalPosition.Y);
		}
	}
}
