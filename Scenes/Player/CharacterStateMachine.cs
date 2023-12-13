using Godot;
using System;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;

public partial class CharacterStateMachine : Node
{
	State[] states;


	[Export]
	public player character;

	[Export]
	public State current_state;

	public bool can_move()
	{
		return current_state.can_move; 
	}
    public override void _Ready()
    {
        foreach(State i in GetChildren())
		{
			if (states.Contains(i))
			{
				states.Append(i);
				i.character = character;
			}
			else
				Console.WriteLine("Child " + i.Name + " is not a State for CharacterStateMachine");
		}

    }

    public override void _PhysicsProcess(double delta)
    {
        if (current_state.next_state != null)
		{
			switch_states(current_state.next_state);
		}
    }

	private void switch_states(State new_state)
	{
		if (current_state.next_state != null)
		{
			current_state.on_enter();
			current_state.next_state = null;
		}
		current_state = new_state;

		current_state.on_exit();
	}
	 
	private void _input(InputEvent even)
	{
		if (current_state == GetNode<GroundState>("Ground"))
			current_state.StateInput(even);
	}
}

