using Godot;
using System;

public partial class GroundState : State
{
    
	[Export]
	public float jump_velocity = -200.0f;
	[Export]
	public float double_jump_velocity = -100.0f;
    [Export]
    public State air_state;

	public override void StateInput(InputEvent even)
	{
		if (even.IsActionPressed("jump"))
		{
			jump();
		}
	}

	private void jump()
	{
		character.velocity.Y = jump_velocity;
        next_state = air_state;
	}

}
