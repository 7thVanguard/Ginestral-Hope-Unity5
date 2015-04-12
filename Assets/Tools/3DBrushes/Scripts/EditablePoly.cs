using UnityEngine;
using System.Collections;


/**
 * Class with the editable poly functions controlled via editor
 */

[ExecuteInEditMode]
public class EditablePoly : MonoBehaviour
{
    [HideInInspector] public bool editable = false;


    public void OnBeginEdit()
    {

    }


    public void OnExitEdit()
    {

    }


    // Called via editor
    public void FreezeTransformations()
    {
        GameObject go = new GameObject();
        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
        Vector3[] vertices = mesh.vertices;
        Vector3 center = GetComponent<MeshFilter>().sharedMesh.bounds.center;

        // Change vertex positions depending on the eulerAngles of the Object.
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = transform.TransformPoint(vertices[i]);
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

        // Destroy this gameObject
        DestroyImmediate(this.gameObject);
    }
}
