using Godot;
using System;

[GlobalClass]
public partial class MasterManager : SceneTree
{
	private static MasterManager instance;
	private static LevelManager levelManager;
	private static SaveManager saveManager;
	

	public override void _Initialize()
	{
		base._Initialize();

		GD.Print("Initialized:");

		instance = Engine.GetMainLoop() as MasterManager;

		saveManager = GetSaveManager;

		saveManager.LoadGame("user://savegame.save");
	}

	public override void _Finalize()
	{
		saveManager.SaveGame("user://savegame.save");

		levelManager = null;
		saveManager = null;
		instance = null;
	}

	public static MasterManager GetMasterManager
	{
		get
		{
			if (instance == null)
			{
				instance = Engine.GetMainLoop() as MasterManager;
			}
			return instance;
		}
	}


	public static LevelManager GetLevelManager
	{
		get
		{
			if (levelManager == null)
			{
				levelManager = new LevelManager();
			}
			return levelManager;
		}
	}

	public static SaveManager GetSaveManager
	{
		get
		{
			if (saveManager == null)
			{
				saveManager = new SaveManager();
			}
			return saveManager;
		}
	}
}
