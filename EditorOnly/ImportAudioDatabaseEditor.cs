
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
    ImportAudioDatabase otherWaffle;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ImportAudioDatabase managedScript = (ImportAudioDatabase)target;
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
        otherWaffle = (ImportAudioDatabase)EditorGUILayout.ObjectField("Target Waffle", otherWaffle, typeof(ImportAudioDatabase), true);
        if (GUILayout.Button("Copy Data")){
            try {
            otherWaffle.ImportAudioData(((ImportAudioDatabase)target).ExportAudioData(), ((ImportAudioDatabase)target).ExportVolumeData());
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
