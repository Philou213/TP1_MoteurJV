using Godot;
using System;

public partial class SaveButton : Button
{
	public override void _Ready()
	{
		this.Pressed += OnPressed;
	}


	public void OnPressed()
	{
		MasterManager.GetSaveManager.SaveGame("user://savegame.save");
	}
}
