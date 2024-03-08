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

	//Runs when a player connects and runs on all peers
	private void PeerConnected(long id)
	{
		GD.Print("Player Connected! " + id.ToString());
	}

	//Runs when a player disconnect and runs on all peers
	private void PeerDisconnected(long id)
	{
		GD.Print("Player Disconnected: " + id.ToString());
	}

	//Runs when the connection failed and only runs on the clients
	private void ConnectionFailed()
	{
		GD.Print("CONNECTION FAILED");
	}

	private void ConnectedToServer()
	{
		GD.Print("Connected To Server");
	}
	
	public override void _Process(double delta){}


	public void _on_host_button_down()
	{
		peer = new ENetMultiplayerPeer();
		var error = peer.CreateServer(port, 2);
		if(error != Error.Ok){
			GD.Print("error cannot host! :" + error.ToString());
			return;
		}
		peer.Host.Compress(ENetConnection.CompressionMode.RangeCoder);

		Multiplayer.MultiplayerPeer = peer;
		GD.Print("Waiting For Players!");
	}

	public void _on_join_button_down()
	{
		peer = new ENetMultiplayerPeer();
		peer.CreateClient(address, port);

		peer.Host.Compress(ENetConnection.CompressionMode.RangeCoder);
		Multiplayer.MultiplayerPeer = peer;
		GD.Print("Joining Game!");
	}


	public void _on_start_game_button_down(){
		Rpc("startGame");
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
	private void startGame(){

		var scene = ResourceLoader.Load<PackedScene>("res://world.tscn").Instantiate<Node>();
		GetTree().Root.AddChild(scene);
		this.Hide();
	}
	
	private void sendPlayerInformation(string name,int id)
	{
		
	}
	
}
