using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

public partial class Dialogue : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public List<string> LoadText()
	{
		return new List<string>();
	}
}
