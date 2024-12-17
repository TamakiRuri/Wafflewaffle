
#if UNITY_EDITOR && !COMPILER_UDONSHARP

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;



[CustomEditor(typeof(WaffleImport))]
public class WaffleImportEditor : Editor
{
    WaffleImport otherWaffle;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        WaffleImport managedScript = (WaffleImport)target;
        if (GUILayout.Button("Generate Data")){
            try {
            managedScript.GenerateDatatoWaffle();
            }
            catch (Exception e){
                throw e;
            }
            Debug.Log("Data Generated Successfully");
        }
        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("Copy Data");
        otherWaffle = (WaffleImport)EditorGUILayout.ObjectField("Target Waffle", otherWaffle, typeof(WaffleImport), true);
        if (GUILayout.Button("Copy Data")){
            try {
            otherWaffle.ImportAudioData(managedScript.ExportAudioData(), managedScript.ExportVolumeData(), managedScript.ExportDoEatData());
            otherWaffle.GenerateDatatoWaffle();
            }
            catch (Exception e){
                throw e;
            }
            Debug.Log("Data Copied Successfully");
        }
    }
    
}
#endif
