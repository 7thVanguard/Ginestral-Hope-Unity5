using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public static class BrushesLib
{
    public static void PrepareObjectComponents(GameObject go, Material material)
    {
        Object.DestroyImmediate(go.GetComponent<MeshRenderer>());
        go.AddComponent<MeshRenderer>();
        go.GetComponent<MeshRenderer>().material = material;
        Object.DestroyImmediate(go.GetComponent<MeshCollider>());
        go.AddComponent<MeshCollider>();
    }
}
