using Godot;
using System;

[GlobalClass]
public partial class LevelManager : Node
{
	public void LoadLevel(string levelName)
	{
		/*if (ResourceLoader.Exists(levelName))
		{
			var tree = SceneTree.GetInstance();
			if (tree != null)
			{
				tree.ChangeSceneToFile(levelName);
			}
		}*/
	}
}
