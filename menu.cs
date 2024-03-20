namespace Skull;

using Godot;
using System;

public partial class menu : Control
{
    public override void _Ready()
    {
		
    }

    public override void _Process(double delta)
    {
    }
    
    private void _on_options_pressed()
    {
        GD.Print("Option button");
        GetTree().ChangeSceneToFile("res://options.tscn");
    }
    
    private void _on_exit_pressed()
    {
        GD.Print("Exit button");
        GetTree().Quit();
    }
    
    private void _on_play_pressed()
    {
        GD.Print("Play button");
        GetTree().ChangeSceneToFile("res://Scenes/Map/world1.tscn");
    }
    private void _on_texture_button_pressed()
    {
        GD.Print("Chat scene");
        GetTree().ChangeSceneToFile("res://chat.tscn");
    }
    
    private void _on_back_pressed()
    {
        GD.Print("Back button");
        GetTree().ChangeSceneToFile("res://menu.tscn");
    }
	
    private void _on_check_button_toggled(bool button_pressed)
    {
		
        var streamPlayer = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
        streamPlayer.Stream = GD.Load<AudioStream>("res://ship-radar.wav");
        if(button_pressed) 
        { 
            GD.Print("CheckButton sound : ", button_pressed);
            streamPlayer.Play();
        }
        else 
        {
            GD.Print("CheckButton sound : ", button_pressed);
            streamPlayer.Stop();
        }
    }
}