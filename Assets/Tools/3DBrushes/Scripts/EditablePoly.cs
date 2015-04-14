using UnityEngine;
using System.Collections;


/**
 * Class with the editable poly functions controlled via editor
 */

[ExecuteInEditMode]
public class EditablePoly : MonoBehaviour
{
    [HideInInspector] public GameObject[] vertexObject;
    [HideInInspector] public bool editable = false;

    public Material material;


    public void OnBeginEdit()
    {
        Vector3 center = GetComponent<MeshFilter>().sharedMesh.bounds.center;
        vertexObject = new GameObject[this.GetComponent<MeshFilter>().sharedMesh.vertices.Length];

        // Vertices creation as gameObjects
        for (int i = 0; i < this.GetComponent<MeshFilter>().sharedMesh.vertices.Length; i++)
        {
            vertexObject[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            vertexObject[i].name = "Vertex " + i;

            vertexObject[i].transform.position = transform.TransformPoint(this.GetComponent<MeshFilter>().sharedMesh.vertices[i]);
            vertexObject[i].transform.parent = transform.parent;
            vertexObject[i].transform.localScale = Vector3.one / 10;

            vertexObject[i].GetComponent<Renderer>().material = material;
        }
    }


    void Update()
    {
        if (enabled)
        {
            
        }
    }


    public void OnExitEdit()
    {

    }


    public void AdaptMesh()
    {
        GameObject go = new GameObject();
        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
        Vector3[] vertices = mesh.vertices;

        for (int i = 0; i < vertexObject.Length; i++)
        {
            vertices[i] = vertexObject[i].transform.position;
            Debug.Log(vertexObject[i].transform.position);
        }

        // Assign the new vertices to our mesh
        mesh.vertices = vertices;

        // Adapt the mesh of the new GameObject
        go.AddComponent<MeshFilter>();
        go.GetComponent<MeshFilter>().mesh = mesh;
        go.AddComponent<MeshRenderer>();
        go.GetComponent<MeshRenderer>().material = this.GetComponent<MeshRenderer>().sharedMaterial;
        go.AddComponent<MeshCollider>();
        go.AddComponent<EditablePoly>();

        // Adapt the transform to default
        go.transform.name = transform.name;
        go.transform.tag = transform.tag;
        go.transform.eulerAngles = Vector3.zero;
        go.transform.position = Vector3.zero;
        go.transform.localScale = Vector3.one;
        go.transform.parent = transform.parent;

        // Assign the children to the new gameObject
        foreach (Transform child in transform)
            child.parent = go.transform;

        // Destroy the gameObjects
        for (int i = 0; i < vertexObject.Length; i++)
            DestroyImmediate(vertexObject[i]);

        // Destroy this gameObject
        DestroyImmediate(this.gameObject);
    }


    // Called via editor
    public void FreezeTransformations()
    {
        GameObject go = new GameObject();
        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
        Vector3[] vertices = mesh.vertices;
        

        // Change vertex positions depending on the eulerAngles of the Object.
        for (int i = 0; i < vertices.Length; i++)
            vertices[i] = transform.TransformPoint(vertices[i]);

        // Assign the new vertices to our mesh
        mesh.vertices = vertices;

        // Adapt the mesh of the new GameObject
        go.AddComponent<MeshFilter>();
        go.GetComponent<MeshFilter>().mesh = mesh;
        go.AddComponent<MeshRenderer>();
        go.GetComponent<MeshRenderer>().material = this.GetComponent<MeshRenderer>().sharedMaterial;
        go.AddComponent<MeshCollider>();
        go.AddComponent<EditablePoly>();

        // Adapt the transform to default
        go.transform.name = transform.name;
        go.transform.tag = transform.tag;
        go.transform.eulerAngles = Vector3.zero;
        go.transform.position = Vector3.zero;
        go.transform.localScale = Vector3.one;
        go.transform.parent = transform.parent;

        // Assign the children to the new gameObject
        foreach (Transform child in transform)
            child.parent = go.transform;

        // Destroy this gameObject
        DestroyImmediate(this.gameObject);
    }
}
