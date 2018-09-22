using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class EditorWindowBase : EditorWindow {


	protected void DoHeader(string title){
		GUILayout.Label (title, EditorStyles.boldLabel);
	}

	protected string DoOpenFolderChooser(string title, string folder){
		EditorGUILayout.BeginHorizontal();
		
		folder = EditorGUILayout.TextField(title,folder);
		if(GUILayout.Button("Open",GUILayout.Width(70))){
			folder =  EditorUtility.OpenFolderPanel(title, folder, "");
		}
		
		EditorGUILayout.EndHorizontal();
		return folder;
	}

	protected void DoTightLabel(string label){
		var w = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector).label.CalcSize(new GUIContent(label)).x;
		GUILayout.Label(label,GUILayout.Width(w));
	}

	protected string DoTextBox(string label, string value)
	{
		return EditorGUILayout.TextField(label,value);
	}

	protected string DoTextArea(string label, string value){
		EditorGUILayout.LabelField(label);
		return EditorGUILayout.TextArea(value);
	}

	protected bool DoCheckBox(string label, bool value, params GUILayoutOption[] options){
		return EditorGUILayout.ToggleLeft(label, value, options);
	}

	protected bool DoButton(string label){
		return GUILayout.Button(label);
	}

	protected void DoHorizontalLine(){
		GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
	}
}
