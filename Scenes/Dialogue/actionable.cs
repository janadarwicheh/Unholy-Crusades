using Godot;
using System;
using DialogueManagerRuntime;

public partial class actionable : Godot.Area2D
{
    [Export] public Resource DialogueRessource;
    [Export] public string DialogueStart = "start";

    public void Action()
    {
        DialogueManager.ShowDialogueBalloon(DialogueRessource, DialogueStart);
    }
}