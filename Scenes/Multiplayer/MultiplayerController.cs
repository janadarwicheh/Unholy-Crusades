using Godot;
using System;
using Skull.Scenes.Multiplayer;
using System.Linq;

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
		if (OS.GetCmdlineArgs().Contains("--server"))
		{
			hostGame();
		}
	}

	private void ConnectionFailed()
	{
		GD.Print("CONNECTION FAILED");
	}

	private void ConnectedToServer()
	{
		GD.Print("CONNECTED TO SERVER");
		RpcId(1,"SendPlayerInformation", GetNode<LineEdit>("LineEdit").Text, Multiplayer.GetUniqueId());
	}

	private void PeerDisconnected(long id)
	{
		GD.Print("PLAYER DISCONNECTED: " + id.ToString());
		GameManager.Players.Remove(GameManager.Players.Where(i => i.Id == id).First<PlayerInfo>());
		var players = GetTree().GetNodesInGroup("Playeru");
		
		foreach (var item in players)
		{
			if (item.Name == id.ToString())
			{
				item.QueueFree();
			}
		}
	}

	private void PeerConnected(long id)
	{
		GD.Print("PLAYER CONNECTED! " + id.ToString());
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void hostGame()
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
	
	public void _on_host_button_down()
	{
		hostGame();
		SendPlayerInformation(GetNode<LineEdit>("LineEdit").Text,1);
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
		foreach (var item in GameManager.Players)
		{
			GD.Print(item.Name + " is playing");
		}

		GetTree().ChangeSceneToFile("res://Scenes/Start/Start.tscn");
		// var scene = ResourceLoader.Load<PackedScene>("res://Scenes/Start/Start.tscn").Instantiate();
		// GetTree().Root.AddChild(scene);
		// this.Hide();
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer)]
	private void SendPlayerInformation(string name, int id)
	{
		PlayerInfo playerInfo = new PlayerInfo()
		{
			Name = name,
			Id = id
		};

		if (!GameManager.Players.Contains(playerInfo))
		{
			GameManager.Players.Add(playerInfo);
		}

		if (Multiplayer.IsServer())
		{
			foreach (var item in GameManager.Players)
			{
				Rpc("SendPlayerInformation", item.Name, item.Id);
			}
		}
	}
}
