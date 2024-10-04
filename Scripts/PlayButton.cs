using Godot;
using System;

public partial class PlayButton : Button
{
	public override void _Ready()
	{
		this.Pressed += OnPressed;
	}


	public void OnPressed()
	{
		MasterManager.GetLevelManager.LoadLevel("res://Scenes/Game.tscn");
	}
}
