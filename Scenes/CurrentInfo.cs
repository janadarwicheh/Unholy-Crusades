﻿using Godot;
using Skull.Scenes.Map;

namespace Skull.Scenes;

public partial class CurrentInfo : Node
{
    public static World CurrentScene;
    public static Playeru player;
    public override void _Ready()
    {
        CurrentScene = (World)GetTree().CurrentScene;
    }
}