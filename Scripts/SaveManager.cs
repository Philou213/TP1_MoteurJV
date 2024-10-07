using Godot;
using System;

public partial class SaveManager : Node
{
	public void LoadGame(string FileName)
	{
		GD.Print("Load:");

		if (!FileAccess.FileExists(FileName))
		{
			return;
		}

		var saveNodes = MasterManager.GetMasterManager.GetNodesInGroup("Persist");
		foreach (Node saveNode in saveNodes)
		{
			saveNode.QueueFree();
		}

		using var saveFile = FileAccess.Open(FileName, FileAccess.ModeFlags.Read);

		while (saveFile.GetPosition() < saveFile.GetLength())
		{
			var jsonString = saveFile.GetLine();

			var json = new Json();
			var parseResult = json.Parse(jsonString);

			if (parseResult != Error.Ok)
			{
				GD.Print($"JSON Parse Error: {json.GetErrorMessage()} in {jsonString} at line {json.GetErrorLine()}");
				continue;
			}

			var nodeData = new Godot.Collections.Dictionary<string, Variant>((Godot.Collections.Dictionary)json.Data);
			var newObjectScene = GD.Load<PackedScene>(nodeData["Filename"].ToString());
			var newObject = newObjectScene.Instantiate<Node>();
			
			//TODO 
			MasterManager.GetMasterManager.GetRoot().GetNode(nodeData["Parent"].ToString()).AddChild(newObject);


			newObject.Set(Node2D.PropertyName.Position, new Vector2((float)nodeData["PosX"], (float)nodeData["PosY"]));

			foreach (var (key, value) in nodeData)
			{
				if (key == "Filename" || key == "Parent" || key == "PosX" || key == "PosY")
				{
					continue;
				}
				newObject.Set(key, value);
			}
		}
	}

	public void SaveGame(string FileName)
	{
		using var saveFile = FileAccess.Open(FileName, FileAccess.ModeFlags.Write);

		var saveNodes = MasterManager.GetMasterManager.GetNodesInGroup("Persist");
		foreach (Node saveNode in saveNodes)
		{
			if (string.IsNullOrEmpty(saveNode.SceneFilePath))
			{
				GD.Print($"persistent node '{saveNode.Name}' is not an instanced scene, skipped");
				continue;
			}

			if (!saveNode.HasMethod("Save"))
			{
				GD.Print($"persistent node '{saveNode.Name}' is missing a Save() function, skipped");
				continue;
			}

			var nodeData = saveNode.Call("Save");
			var jsonString = Json.Stringify(nodeData);

			saveFile.StoreLine(jsonString);
		}
	}
}
