using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using UnityEditor.SceneManagement;

public static class CreateBaseFolderContextMenu {

	[MenuItem("Assets/GameJam/MakeBaseFolder", false, 999)]
	static void MakeBaseFolder()
	{
		MakeFolder("Prefabs");
		MakeFolder("Scripts");
		MakeFolder("Sprites");
		MakeFolder("Shaders");
		MakeFolder("Sounds");
		MakeFolder("Scenes",false);
		MakeFolder("Scenes/Game",false);
		MakeFolder("Scenes/Gyms",false);
		MakeFolder("Materials");
		MakeFolder("Textures");
		MakeFolder("Resources");
		MakeFolder("Plugins");
		MakeFolder("Fonts");
		MakeScene("Scenes/Gyms","Richard");
		MakeScene("Scenes/Gyms","Kevin");
		MakeScene("Scenes/Game","Menu");
		MakeScene("Scenes/Game","Credit");
		MakeScene("Scenes/Game","Game");
		
	}

	private static void MakeScene(string path, string name)
	{
		var newScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects);
		EditorSceneManager.SaveScene(newScene,"Assets/" + path + "/" + name +".unity");
	}

	static void MakeFolder(string name, bool makeEmptyText = true)
	{
		var pathFolder = Application.dataPath + "/" + name;
		var pathEmptyFile = pathFolder + "/empty.txt";

		if(!Directory.Exists(pathFolder)){
			Directory.CreateDirectory(pathFolder);
			if(!File.Exists(pathEmptyFile) && makeEmptyText)
			{
				var fs = File.Create(pathEmptyFile);
				fs.Close();
			}
		}
	}
}
