#if UNITY_EDITOR && !COMPILER_UDONSHARP
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportAudioDatabase : MonoBehaviour
{
    [SerializeField] private AudioList[] audioList;
    //[SerializeField] private bool isManual;
    [SerializeField] private GameObject waffle;

    public void ImportAudioData(AudioClip[] l_clips, float[] l_volume, bool[] l_doEats){
        AudioList[] l_list = new AudioList[l_clips.Length];
        for (int i = 0; i < l_clips.Length; i++){
            AudioList l_entry = new AudioList(l_clips[i], l_volume[i], l_doEats[i]);
            l_list[i] = l_entry;
        }
        audioList = l_list;
    }
    public AudioClip[] ExportAudioData(){
        AudioClip[] l_clips = new AudioClip[audioList.Length];
        for (int i = 0; i < audioList.Length; i++){
            l_clips[i] = audioList[i].audioClip;
        }
        return l_clips;
    }
    public float[] ExportVolumeData(){
        float[] l_volume = new float[audioList.Length];
        for (int i = 0; i < audioList.Length; i++){
            l_volume[i] = audioList[i].volume;
        }
        return l_volume;
    }
    public bool[] ExportDoEatData(){
        bool[] l_doEats = new bool[audioList.Length];
        for (int i = 0; i < audioList.Length; i++){
            l_doEats[i] = audioList[i].doEat;
        }
        return l_doEats;
    }
    public void GenerateDatatoWaffle(){
        // if (isManual) {
            WaffleWaffleManual waffleClass = waffle.GetComponent<WaffleWaffleManual>();
            waffleClass.ImportDatas(ExportAudioData(),ExportVolumeData(),ExportDoEatData());
        // }
        // else {
        //     WaffleWaffle waffleClass = gameObject.GetComponent<WaffleWaffle>();
        //     waffleClass.ImportDatas(ExportAudioData(),ExportVolumeData(),ExportDoEatData());
        // }
    }
}

[Serializable]public class AudioList{
    public AudioList(AudioClip l_clip = null, float l_volume = 0, bool l_doEat = false){
        audioClip = l_clip;
        volume = l_volume;
        doEat = l_doEat;
    }
    public AudioClip audioClip;
    [Range(0,1)]
    public float volume;
    public bool doEat;
}
#endif