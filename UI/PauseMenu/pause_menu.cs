using Godot;
using System;
using System.Threading;

public partial class pause_menu : Control
{
	private CanvasLayer Parent;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Parent = GetParent<CanvasLayer>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		TestEsc();
	}
	
	public void _Resume()
	{
		GetTree().Paused = false;
		Parent.Visible = false;
	}
	
	public void _Pause()
	{
		GetTree().Paused = true;
		Parent.Visible = true;
	}
	
	public void TestEsc()
	{
		if (Input.IsActionJustPressed("esc") && !GetTree().Paused)
		{
			_Pause();
		}
		else if (Input.IsActionJustPressed("esc") && GetTree().Paused)
		{
			_Resume();
		}
	}
	
	private void _on_resume_pressed()
	{
		_Resume();
	}
	
	private void _on_restart_pressed()
	{
		GetTree().ReloadCurrentScene();
	}
	
	private void _on_quit_pressed()
	{
		GetTree().Quit();
	}
}
