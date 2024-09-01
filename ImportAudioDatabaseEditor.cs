
#if UNITY_EDITOR && !COMPILER_UDONSHARP

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;



[CustomEditor(typeof(ImportAudioDatabase))]
public class ImportAudioDatabaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ImportAudioDatabase managedScript = (ImportAudioDatabase)target;
        if (GUILayout.Button("Generate Data")){
            try {
            managedScript.generateDatatoWaffle();
            }
            catch (Exception e){
                throw e;
            }
            Debug.Log("Data Generated Successfully");
        }
    }
    
}
#endif
