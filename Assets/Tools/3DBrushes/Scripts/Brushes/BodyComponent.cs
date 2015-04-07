using UnityEngine;
using UnityEditor;
using System.Collections;


/**
 * This class is used in the subcomponents of the brush controller to redirect the selection to it.
 */
[ExecuteInEditMode]
public class BodyComponent : MonoBehaviour 
{
	private GameObject go;


	public void Init(GameObject go)
	{
		this.go = go;
	}

	
	void Update () 
	{
		if (Selection.activeGameObject == this.gameObject)
			Selection.activeGameObject = go;
	}
}
