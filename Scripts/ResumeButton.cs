using Godot;
using System;

public partial class ResumeButton : Button
{
	public event Action ResumeButtonClicked;
	
	public override void _Ready()
	{
		this.Pressed += OnPressed;
	}


	public void OnPressed()
	{
		ResumeButtonClicked?.Invoke();
	}
}
