using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Authentication;

public partial class Enemies : Node
{
	PathFollow2D path;
	enemy knight1;
	bool paused = false;
	[Export]
	public PackedScene knight1Scene;
	// Called when the node enters the scene tree for the first time.
	public void halt()
	{
		paused = true;
	}
	public void start()
	{
		paused = false;
	}
	public override void _Ready()
	{
		path = GetNode<PathFollow2D>("Path2D/PathFollow2D");
		knight1 = GetNode<enemy>("Path2D/PathFollow2D/Enemy1");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!paused)
			{
				path.Progress += 25.0f * (float)delta;
			if (path.ProgressRatio>0.5f)
			{
				knight1.animation.FlipH = true;
			}
			else
				knight1.animation.FlipH = false;
			}	
	}
}
