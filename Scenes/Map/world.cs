using Godot;

namespace Skull.Scenes.Map;

public partial class World : Node
{
	public Playeru player;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (player.GlobalPosition.Y > 1000)
		{
			player.GlobalPosition = new Vector2(215, 190);
		}
	}
}