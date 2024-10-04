using Godot;
using System;

public partial class ExitButton : Button
{
	public override void _Ready()
	{
		this.Pressed += OnPressed;
	}


	public void OnPressed()
	{
		GetTree().Quit();
	}
}
