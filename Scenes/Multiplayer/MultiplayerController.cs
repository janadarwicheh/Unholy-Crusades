using Godot;
using System;

public partial class MultiplayerController : Control 

{
	[Export]
	private int port = 8910;
	
    [Export]
	private string address = "127.0.0.1";

	private ENetMultiplayerPeer peer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Multiplayer.PeerConnected += PeerConnected;
		Multiplayer.PeerDisconnected += PeerDisconnected;
		Multiplayer.ConnectedToServer += ConnectedToServer;
		Multiplayer.ConnectionFailed += ConnectionFailed;
	}

	private void ConnectionFailed()
	{
		GD.Print("CONNECTION FAILED");
	}

	private void ConnectedToServer()
	{
		GD.Print("CONNECTED TO SERVER");
	}

	private void PeerDisconnected(long id)
	{
		GD.Print("PLAYER DISCONNECTED: " + id.ToString());
	}

	private void PeerConnected(long id)
	{
		GD.Print("PLAYER CONNECTED! " + id.ToString());
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void _on_host_button_down()
	{
		peer = new ENetMultiplayerPeer();
		var error = peer.CreateServer(port, 2);
		
		if (error != Error.Ok)
		{
			GD.Print("ERROR CANNOT HOST: " + error.ToString());
			return;
		}
		
		peer.Host.Compress(ENetConnection.CompressionMode.RangeCoder);

		Multiplayer.MultiplayerPeer = peer;
		GD.Print("WAITING FOR PLAYERS!");
	}

	public void _on_join_button_down()
	{
		peer = new ENetMultiplayerPeer();
		peer.CreateClient(address, port);
		
		peer.Host.Compress(ENetConnection.CompressionMode.RangeCoder);
		Multiplayer.MultiplayerPeer = peer;
		GD.Print("JOINING GAME...");
	}

	public void _on_start_game_button_down()
	{
		Rpc("StartGame");
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
	
	private void StartGame()
	{
		var scene = ResourceLoader.Load<PackedScene>("res://Scenes/Map/world1.tscn").Instantiate();
		GetTree().Root.AddChild(scene);
		this.Hide();
	}

	private void SendPlayerInformation(string name, int id)
	{
		throw new NotImplementedException();
	}
}
