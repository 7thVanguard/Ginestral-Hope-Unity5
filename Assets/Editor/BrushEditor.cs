using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomEditor(typeof(BrushMesh))]
public class BrushEditor : Editor
{
	public override void OnInspectorGUI()
	{
		BrushMesh script = (BrushMesh)target;

		if (GUILayout.Button("Create"))
		    script.Create();

		DrawDefaultInspector();
	}
}
