using UnityEngine;
using UnityEditor;

public static class EditorUtils
{

	public static void BoolField(string label, string tooltip, ref bool value){
		value = EditorGUILayout.Toggle(new GUIContent(label,tooltip),value);
	}

	public static void FloatField(string label, string tooltip, ref float value, float min, float max)
	{
		var val = EditorGUILayout.FloatField(new GUIContent(label,tooltip), value);
		value = Mathf.Clamp(val, min, max);
	}
	public static void FloatField(string label, string tooltip, ref float value)
	{
		value = EditorGUILayout.FloatField(new GUIContent(label,tooltip), value);
	}

	public static void Vector2Field(string label, string tooltip, ref Vector2 value){
		value = EditorGUILayout.Vector2Field(new GUIContent(label, tooltip),value);
	}
}
