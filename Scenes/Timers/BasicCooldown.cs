using System;
using Godot;
using Newtonsoft.Json.Serialization;

namespace Skull.Scenes.Timers;

public partial class BasicCooldown : Timer
{
	// Called when the node enters the scene tree for the first time.*
	private float BasicTime { get; set; }
	public BasicCooldown(float basicTime)
	{
		BasicTime = basicTime;
		Autostart = false;
		OneShot = true;
	}
	public override void _Ready()
	{
		base._Ready();
		Timeout += OnTimeout();
	}
	private Action OnTimeout()
	{
		if (OneShot)
			Stop();
		return () => { };
	}
	
	public void Create(float time = -1, bool autostart = false, bool oneshot = true)
	{
		if (time == -1)
			time = BasicTime;
		WaitTime = time;
		Autostart = autostart;
		OneShot = oneshot;
		Start();
	}
	
}