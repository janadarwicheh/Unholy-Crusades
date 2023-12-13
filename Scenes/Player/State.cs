using Godot;
using System;

public abstract partial class State : Node
{
	[Export]
    public bool can_move = true;

    public State next_state;

    [Export]
    public player character;

    public void on_enter()
    {

    }

    public void on_exit()
    {

    }

    public abstract void StateInput(InputEvent even);
}
