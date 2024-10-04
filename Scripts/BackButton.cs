using Godot;
using System;

public partial class BackButton : Button
{
	public event Action BackButtonClicked;
	
	public override void _Ready()
	{
		this.Pressed += OnPressed;
	}


	public void OnPressed()
	{
		BackButtonClicked?.Invoke();
	}
}
