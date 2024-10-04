using Godot;
using System;

public partial class LevelManager : Node
{
	public void LoadLevel(string levelName)
	{
		PackedScene packedScene = ResourceLoader.Load<PackedScene>(levelName);

		if (packedScene != null)
		{
			Node newLevel = packedScene.Instantiate();
			MasterManager.GetMasterManager.Root.AddChild(newLevel);
			
			Node currentScene = MasterManager.GetMasterManager.CurrentScene;
			if (currentScene != null)
			{
				currentScene.QueueFree(); // Safely remove the current scene after the new one is added
			}

			// Set the new scene as the active one
			MasterManager.GetMasterManager.CurrentScene = newLevel;
		}
	}
}
