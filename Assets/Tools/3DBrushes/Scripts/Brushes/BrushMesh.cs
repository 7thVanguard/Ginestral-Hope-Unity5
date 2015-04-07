using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


[ExecuteInEditMode]
public class BrushMesh : MonoBehaviour
{
	// External
	// Classes
	public BrushGridAdapt gridAdapt = new BrushGridAdapt();

	// Structs
	public BrushStairsVariables brushStairsVariables;

	// Enums
	public Brush selectedBrush;
	public PivotPointInX pivotPointInX;
	public PivotPointInY pivotPointInY;
	public PivotPointInZ pivotPointInZ;

	// Internal
	// GameObjects
	private GameObject ghost;
	private GameObject body;
	private GameObject parentObj;

	// Rendering
	public Material alphaMaterial;
	public Material opaqueMaterial;

	// Generic
	public Vector3 size;
	public Vector3 eulerAngles;


	void Update () 
	{
		// Brush creation
		if (transform.parent == null)
		{
			Init ();
		}
		else
		{
			// Grid adapting
			transform.position = GridPositionAdapt();
			transform.eulerAngles = GridRotationAdapt();
			ghost.transform.position = transform.position;
			ghost.transform.eulerAngles = transform.eulerAngles;

			// Set transforms
			parentObj.transform.position = Vector3.zero;
			body.transform.position = Vector3.zero;

			// Draw the ghost mesh
			DrawGhost();
		}
	}
	

	void DrawGhost()
	{
		switch(selectedBrush)
		{
		case Brush.Cube:
			ghost.GetComponent<MeshFilter>().mesh = BrushCube.Ghost(pivotPointInX, pivotPointInY, pivotPointInZ, 
			                                                        eulerAngles, size);
            BrushesLib.PrepareObjectComponents(ghost, alphaMaterial);
			break;
		case Brush.Stairs:
			ghost.GetComponent<MeshFilter>().mesh = BrushStairs.Ghost(pivotPointInX, pivotPointInY, pivotPointInZ, 
			                                                          eulerAngles, size, brushStairsVariables);
            BrushesLib.PrepareObjectComponents(ghost, alphaMaterial);
			break;
		default:
			ghost.GetComponent<MeshFilter>().mesh = new Mesh();
            BrushesLib.PrepareObjectComponents(ghost, alphaMaterial);
			break;
		}
	}


	// Called via Inspector
	public void Create()
	{
		DrawGhost ();

		// Assign mesh to a new gameObject
		GameObject childObj = new GameObject();
		childObj.transform.position = ghost.transform.position;
		childObj.transform.parent = body.transform;
		childObj.AddComponent<MeshFilter>();
		childObj.GetComponent<MeshFilter>().mesh = ghost.GetComponent<MeshFilter>().sharedMesh;
		childObj.AddComponent<MeshRenderer>();
		childObj.AddComponent<MeshCollider>();
		
		Combine ();
	}


	public void Combine()
	{
		MeshFilter[] meshFilters = body.transform.GetComponentsInChildren<MeshFilter>();
		CombineInstance[] combine = new CombineInstance[meshFilters.Length];

		for (int i = 0; i < meshFilters.Length; i++)
		{
			combine[i].mesh = meshFilters[i].sharedMesh;
			combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
		}

		body.transform.GetComponent<MeshFilter>().sharedMesh = new Mesh();
		body.transform.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combine);

		foreach(Transform child in body.transform)
			DestroyImmediate(child.gameObject);

		BrushesLib.PrepareObjectComponents(body, opaqueMaterial);
	}
	
	
	// Auxiliar methods
	// --------------------------------------------------------------------
	private void Init()
	{
		// Create the gameObjects
		this.gameObject.name = "Controller";
		
		ghost = new GameObject();
		ghost.name = "Ghost";
		
		body = new GameObject();
		body.name = "Body";
		
		parentObj = new GameObject();
		parentObj.name = "Brush";
		
		// Transforms
		transform.position = Vector3.zero;
		ghost.transform.position = Vector3.zero;
		body.transform.position = Vector3.zero;
		parentObj.transform.position = Vector3.zero;
		
		transform.parent = parentObj.transform;
		ghost.transform.parent = parentObj.transform;
		body.transform.parent = parentObj.transform;
		
		// Components
		ghost.AddComponent<MeshFilter>();
		ghost.AddComponent<MeshRenderer>();
		ghost.AddComponent<MeshCollider>();
		
		ghost.AddComponent<BodyComponent>();
		ghost.GetComponent<BodyComponent>().Init(this.gameObject);

		body.AddComponent<MeshFilter>();
		body.AddComponent<MeshRenderer>();
		body.AddComponent<MeshCollider>();

		body.AddComponent<BodyComponent>();
		body.GetComponent<BodyComponent>().Init(this.gameObject);
		
		parentObj.AddComponent<BodyComponent>();
		parentObj.GetComponent<BodyComponent>().Init(this.gameObject);

		// Generic
		size = Vector3.one * 2;

		// Grid
		gridAdapt.gridSnapPositionSensitivity = 1;
		gridAdapt.gridSnapRotationnSensitivity = 15;
		gridAdapt.gridDisplacement = Vector3.zero;

		// Stairs
		brushStairsVariables.steps = 8;
	}


	private Vector3 GridPositionAdapt()
	{
		Vector3 gridSnapDisplacement;
		gridSnapDisplacement.x = transform.position.x % gridAdapt.gridSnapPositionSensitivity + gridAdapt.gridDisplacement.x;
		gridSnapDisplacement.y = transform.position.y % gridAdapt.gridSnapPositionSensitivity + gridAdapt.gridDisplacement.y;
		gridSnapDisplacement.z = transform.position.z % gridAdapt.gridSnapPositionSensitivity + gridAdapt.gridDisplacement.z;
		Vector3 objectivePosition = transform.position;
		
		if (gridSnapDisplacement.x != 0)
		{
			if (gridSnapDisplacement.x < gridAdapt.gridSnapPositionSensitivity / 2)
				objectivePosition.x = transform.position.x - gridSnapDisplacement.x;
			else
				objectivePosition.x = transform.position.x + gridAdapt.gridSnapPositionSensitivity - gridSnapDisplacement.x;
		}
		if (gridSnapDisplacement.y != 0)
		{
			if (gridSnapDisplacement.y < gridAdapt.gridSnapPositionSensitivity / 2)
				objectivePosition.y = transform.position.y - gridSnapDisplacement.y;
			else
				objectivePosition.y = transform.position.y + gridAdapt.gridSnapPositionSensitivity - gridSnapDisplacement.y;
		}
		if (gridSnapDisplacement.z != 0)
		{
			if (gridSnapDisplacement.z < gridAdapt.gridSnapPositionSensitivity / 2)
				objectivePosition.z = transform.position.z - gridSnapDisplacement.z;
			else
				objectivePosition.z = transform.position.z + gridAdapt.gridSnapPositionSensitivity - gridSnapDisplacement.z;
		}
		
		return objectivePosition;
	}


	private Vector3 GridRotationAdapt()
	{
		Vector3 gridSnapDisplacement;
		gridSnapDisplacement.x = transform.eulerAngles.x % gridAdapt.gridSnapRotationnSensitivity;
		gridSnapDisplacement.y = transform.eulerAngles.y % gridAdapt.gridSnapRotationnSensitivity;
		gridSnapDisplacement.z = transform.eulerAngles.z % gridAdapt.gridSnapRotationnSensitivity;
		Vector3 objectiveRotation = transform.eulerAngles;

		if (gridSnapDisplacement.x != 0)
		{
			if (gridSnapDisplacement.x < gridAdapt.gridSnapPositionSensitivity / 2)
				objectiveRotation.x = transform.eulerAngles.x - gridSnapDisplacement.x;
			else
				objectiveRotation.x = transform.eulerAngles.x + gridAdapt.gridSnapRotationnSensitivity - gridSnapDisplacement.x;
		}
		if (gridSnapDisplacement.y != 0)
		{
			if (gridSnapDisplacement.y < gridAdapt.gridSnapPositionSensitivity / 2)
				objectiveRotation.y = transform.eulerAngles.y - gridSnapDisplacement.y;
			else
				objectiveRotation.y = transform.eulerAngles.y + gridAdapt.gridSnapRotationnSensitivity - gridSnapDisplacement.y;
		}
		if (gridSnapDisplacement.z != 0)
		{
			if (gridSnapDisplacement.z < gridAdapt.gridSnapPositionSensitivity / 2)
				objectiveRotation.z = transform.eulerAngles.z - gridSnapDisplacement.z;
			else
				objectiveRotation.z = transform.eulerAngles.z + gridAdapt.gridSnapRotationnSensitivity - gridSnapDisplacement.z;
		}

		return objectiveRotation;
	}
}


public enum Brush { None, Cube, Stairs };
public enum PivotPointInX { Central, Left, Right };
public enum PivotPointInY { Central, Top, Bottom };
public enum PivotPointInZ { Central, Front, Back };


[System.Serializable]
public class BrushGridAdapt
{
	public float gridSnapPositionSensitivity;
	public float gridSnapRotationnSensitivity;
	public Vector3 gridDisplacement;
}


[System.Serializable]
public struct BrushStairsVariables
{
	public int steps;
}



































