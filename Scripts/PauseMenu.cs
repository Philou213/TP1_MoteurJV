using Godot;
using System;

public partial class PauseMenu : Control
{
	private bool isGamePaused = false;
	
	public override void _Ready()
	{
		BackButton backButton = GetNode<BackButton>("ButtonContainer/BackButton");
		backButton.BackButtonClicked += GoBackToMainMenu;
		
		ResumeButton resumeButton = GetNode<ResumeButton>("ButtonContainer/ResumeButton");
		resumeButton.ResumeButtonClicked += TogglePause;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("pause")) TogglePause();
	}
	
	public void TogglePause()
	{
		//Resume game
		if (isGamePaused) 
		{
			Engine.TimeScale = 1f;
			Visible = false;
		}
		//Pause game
		else
		{
			Engine.TimeScale = 0f;
			Visible = true;
		}
		isGamePaused = !isGamePaused;
	}
	
	public void GoBackToMainMenu()
	{
		TogglePause();
		MasterManager.GetLevelManager.LoadLevel("res://Scenes/MainMenu.tscn");
	}
}
