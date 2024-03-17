using Godot;
using System;
using Skull.Scenes.Map;
using System.ComponentModel;
using Skull.Scenes.Player;

public partial class world1 : World
{
    private Playeru _joueur;
    private PackedScene tamer;
    private bool CharacterChosen = false;
    public override void _Ready()
    {
        base._Ready();
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
                tamer = (PackedScene)GD.Load("res://Scenes/Player/Eldric.tscn");
                CharacterChosen = true;
            }
            if (CharacterChosen)
            {
                _joueur = (Playeru)tamer.Instantiate();
                Camera2D cam = new Camera2D();
                cam.PositionSmoothingEnabled = true;
                _joueur.AddChild(cam);
                _joueur.Position = new Vector2(0, 0);
                GetNode("Player/RemoteTransform2D").AddChild(_joueur);
                var a = (RemoteTransform2D)GetNode("Player/RemoteTransform2D");
                a.RemotePath = _joueur.GetPath();
                a.UpdateScale = false;
                player = _joueur;
            }
        }
        else
        {
            base._Process(delta);
        }
    }
}
