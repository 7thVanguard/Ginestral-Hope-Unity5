using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Class that draws ghost stairs when it's selected in the brush controller
[ExecuteInEditMode]
public class BrushStairs : MonoBehaviour 
{
	public static Mesh Ghost(PivotPointInX pivotX, PivotPointInY pivotY, PivotPointInZ pivotZ,
	                         Vector3 eulerAngles, Vector3 size, BrushStairsVariables stairs)
	{
		Mesh mesh;
		
		List<Vector3> Vertices = new List<Vector3> ();
		List<Vector2> UV = new List<Vector2> ();
		List<int> Triangles = new List<int> ();
		
		Vertices = SetVertices(pivotX, pivotY, pivotZ, Vertices, eulerAngles, size, stairs);
		UV = SetUV(UV, size, stairs);
		Triangles = SetTriangles(Triangles, size, stairs);
		mesh = SetMesh (Vertices, UV, Triangles);
		
		return mesh;
	}
	
	
	public static List<Vector3> SetVertices(PivotPointInX pivotX, PivotPointInY pivotY, PivotPointInZ pivotZ, 
	                                        List<Vector3> Vertices, Vector3 eulerAngles,  Vector3 size, 
	                                        BrushStairsVariables stairs)
	{
		// Setting the pivot point
		Vector3 pivot = Vector3.zero;
		
		switch (pivotX) 
		{
		case PivotPointInX.Central:
			pivot.x = 0;
			break;
		case PivotPointInX.Left:
			pivot.x = (size.x / 2);
			break;
		case PivotPointInX.Right:
			pivot.x = -(size.x / 2);
			break;
		default:
			break;
		}
		
		switch (pivotY) 
		{
		case PivotPointInY.Central:
			pivot.y = 0;
			break;
		case PivotPointInY.Top:
			pivot.y = (size.y / 2);
			break;
		case PivotPointInY.Bottom:
			pivot.y = -(size.y / 2);
			break;
		default:
			break;
		}
		
		switch (pivotZ) 
		{
		case PivotPointInZ.Central:
			pivot.z = 0;
			break;
		case PivotPointInZ.Front:
			pivot.z = (size.z / 2);
			break;
		case PivotPointInZ.Back:
			pivot.z = -(size.z / 2);
			break;
		default:
			break;
		}

		// Top
		for (int i = 0; i < stairs.steps; i++)
		{
			Vertices.Add(new Vector3((size.x / 2), 	
			                         -(size.y / 2) + (stairs.steps - i) * size.y / stairs.steps, 	
			                         (size.z / 2) - (i + 1) * size.z / stairs.steps) + pivot);

			Vertices.Add(new Vector3((size.x / 2), 	
			                         -(size.y / 2) + (stairs.steps - i) * size.y / stairs.steps, 	
			                         (size.z / 2) - i * size.z / stairs.steps) + pivot);

			Vertices.Add(new Vector3(-(size.x / 2), 
			                         -(size.y / 2) + (stairs.steps - i) * size.y / stairs.steps, 	
			                         (size.z / 2) - i * size.z / stairs.steps) + pivot);

			Vertices.Add(new Vector3(-(size.x / 2), 
			                         -(size.y / 2) + (stairs.steps - i) * size.y / stairs.steps, 	
			                         (size.z / 2) - (i + 1) * size.z / stairs.steps) + pivot);
		
			// Front
			Vertices.Add(new Vector3((size.x / 2), 	
			                         -(size.y / 2) + i * size.y / stairs.steps,						
			                         -(size.z / 2) + i * size.z / stairs.steps) + pivot);

			Vertices.Add(new Vector3((size.x / 2), 	
			                         -(size.y / 2) + (i + 1) * size.y / stairs.steps, 				
			                         -(size.z / 2) + i * size.z / stairs.steps) + pivot);

			Vertices.Add(new Vector3(-(size.x / 2), 
			                         -(size.y / 2) + (i + 1) * size.y / stairs.steps, 				
			                         -(size.z / 2) + i * size.z / stairs.steps) + pivot);

			Vertices.Add(new Vector3(-(size.x / 2), 
			                         -(size.y / 2) + i * size.y / stairs.steps, 						
			                         -(size.z / 2) + i * size.z / stairs.steps) + pivot);
		
			// Right
			Vertices.Add(new Vector3((size.x / 2), 
			                         -(size.y / 2), 													
			                         -(size.z / 2) + (i + 1) * size.z / stairs.steps) + pivot);

			Vertices.Add(new Vector3((size.x / 2), 
			                         -(size.y / 2) + (i + 1) * size.y / stairs.steps, 				
			                         -(size.z / 2) + (i + 1) * size.z / stairs.steps) + pivot);

			Vertices.Add(new Vector3((size.x / 2), 
			                         -(size.y / 2) + (i + 1) * size.y / stairs.steps, 				
			                         -(size.z / 2) + i * size.z / stairs.steps) + pivot);

			Vertices.Add(new Vector3((size.x / 2), 
			                         -(size.y / 2), 													
			                         -(size.z / 2) + i * size.z / stairs.steps) + pivot);

			// Left
			Vertices.Add(new Vector3(-(size.x / 2), 
			                         -(size.y / 2), 													
			                         -(size.z / 2) + i * size.z / stairs.steps) + pivot);

			Vertices.Add(new Vector3(-(size.x / 2), 
			                         -(size.y / 2) + (i + 1) * size.y / stairs.steps, 				
			                         -(size.z / 2) + i * size.z / stairs.steps) + pivot);

			Vertices.Add(new Vector3(-(size.x / 2), 
			                         -(size.y / 2) + (i + 1) * size.y / stairs.steps, 				
			                         -(size.z / 2) + (i + 1) * size.z / stairs.steps) + pivot);

			Vertices.Add(new Vector3(-(size.x / 2), 
			                         -(size.y / 2), 													
			                         -(size.z / 2) + (i + 1) * size.z / stairs.steps) + pivot);
		
			// Bot
			Vertices.Add(new Vector3((size.x / 2), 	
			                         -(size.y / 2), 													
			                         -(size.z / 2) + (i + 1) * size.z / stairs.steps) + pivot);

			Vertices.Add(new Vector3((size.x / 2), 	
			                         -(size.y / 2), 													
			                         -(size.z / 2) + i * size.z / stairs.steps) + pivot);

			Vertices.Add(new Vector3(-(size.x / 2), 
			                         -(size.y / 2), 													
			                         -(size.z / 2) + i * size.z / stairs.steps) + pivot);

			Vertices.Add(new Vector3(-(size.x / 2), 
			                         -(size.y / 2), 													
			                         -(size.z / 2) + (i + 1) * size.z / stairs.steps) + pivot);
		}

		// Back
		Vertices.Add(new Vector3(-(size.x / 2), 
		                         -(size.y / 2), 	
		                         (size.z / 2)) + pivot);

		Vertices.Add(new Vector3(-(size.x / 2), 
		                         (size.y / 2), 	
		                         (size.z / 2)) + pivot);

		Vertices.Add(new Vector3((size.x / 2), 	
		                         (size.y / 2), 	
		                         (size.z / 2)) + pivot);

		Vertices.Add(new Vector3((size.x / 2), 	
		                         -(size.y / 2), 	
		                         (size.z / 2)) + pivot);

		// Rotation adapt
		for (int i = 0; i < Vertices.Count; i++)
			Vertices[i] = Quaternion.Euler(eulerAngles) * Vertices[i];

		return Vertices;
	}
	
	
	public static List<Vector2> SetUV(List<Vector2> UV, Vector3 size, BrushStairsVariables stairs)
	{
		// Top
		for (int i = 0; i < stairs.steps; i++)
		{
			UV.Add(new Vector2(0, 0));
			UV.Add(new Vector2(0, 1));
			UV.Add(new Vector2(1, 1));
			UV.Add(new Vector2(1, 0));
		
			// Front
			UV.Add(new Vector2(0, 0));
			UV.Add(new Vector2(0, 1));
			UV.Add(new Vector2(1, 1));
			UV.Add(new Vector2(1, 0));
		
			// Right
			UV.Add(new Vector2(0, 0));
			UV.Add(new Vector2(0, 1));
			UV.Add(new Vector2(1, 1));
			UV.Add(new Vector2(1, 0));

			// Left
			UV.Add(new Vector2(0, 0));
			UV.Add(new Vector2(0, 1));
			UV.Add(new Vector2(1, 1));
			UV.Add(new Vector2(1, 0));
		
			// Bot
			UV.Add(new Vector2(0, 0));
			UV.Add(new Vector2(0, 1));
			UV.Add(new Vector2(1, 1));
			UV.Add(new Vector2(1, 0));
		}
		
		// Back
		UV.Add(new Vector2(0, 0));
		UV.Add(new Vector2(0, 1));
		UV.Add(new Vector2(1, 1));
		UV.Add(new Vector2(1, 0));
		
		return UV;
	}
	
	
	public static List<int> SetTriangles(List<int> Triangles, Vector3 size, BrushStairsVariables stairs)
	{

		for (int i = 0; i < 5 * stairs.steps + 1; i++)
		{
			int tri = i * 4;

			Triangles.Add(tri + 0);
			Triangles.Add(tri + 2);
			Triangles.Add(tri + 1);
			
			Triangles.Add(tri + 0);
			Triangles.Add(tri + 3);
			Triangles.Add(tri + 2);
		}
		
		return Triangles;
	}
	
	
	private static Mesh SetMesh(List<Vector3> Vertices, List<Vector2> UV, List<int> Triangles)
	{
		Mesh mesh = new Mesh();
		
		// Prepare arrays to create the mesh
		Vector3[] verticesVector = new Vector3[Vertices.Count];
		Vector2[] UVVecor = new Vector2[UV.Count];
		int[] trianglesVector = new int[Triangles.Count];
		
		// Assign vertices
		for (int i = 0; i < Vertices.Count; i++)
		{
			verticesVector[i] = Vertices[i];
			UVVecor[i] = UV[i];
		}
		// Assign triangles
		for (int i = 0; i < Triangles.Count; i++)
			trianglesVector[i] = Triangles[i];
		
		// Clear the lists
		Vertices.Clear();
		UV.Clear();
		Triangles.Clear();
		
		// Assign the information in the arrays to the mesh
		mesh.vertices = verticesVector;
		mesh.uv = UVVecor;
		mesh.triangles = trianglesVector;
		
		// Calculate normals
		mesh.RecalculateNormals();
		
		return mesh;
	}
}
