using Godot;
using System;
public partial class QUIT : Control
{
	private void _on_quit_pressed()
	{
		GD.Print("Exit button");
		GetTree().Quit();
	}
	
}
