using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomEditor(typeof(EditablePoly))]
public class EditablePolyEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        EditablePoly script = (EditablePoly)target;

        if (!script.editable)
        {
            if (GUILayout.Button("Convert To Editable"))
                script.editable = true;
        }
        else
        {
            if (GUILayout.Button("Finish editing"))
                script.editable = false;
            if (GUILayout.Button("Freeze Transformations"))
                script.FreezeTransformations();
        }


        DrawDefaultInspector();
    }
}
