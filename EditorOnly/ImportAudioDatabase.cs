#if UNITY_EDITOR && !COMPILER_UDONSHARP
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRC.Udon;

public class ImportAudioDatabase : MonoBehaviour
{
    [SerializeField] private AudioList[] audioList;
    [SerializeField] private bool isManual;
    [SerializeField] private GameObject waffle;

    public void ImportAudioData(AudioClip[] l_clips, float[] l_volume){
        AudioList[] l_list = new AudioList[l_clips.Length];
        for (int i = 0; i < l_clips.Length; i++){
            AudioList l_entry = new AudioList(l_clips[i], l_volume[i]);
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
    public void GenerateDatatoWaffle(){
        UdonBehaviour waffleUdon;
        if (isManual) waffleUdon = (UdonBehaviour)waffle.GetComponent(typeof(UdonBehaviour));
        else waffleUdon = (UdonBehaviour)gameObject.GetComponent(typeof(UdonBehaviour));
        waffleUdon.SendMessage("ImportAudio", ExportAudioData());
        waffleUdon.SendMessage("ImportVolume", ExportVolumeData());
    }
}

[Serializable]public class AudioList{
    public AudioList(AudioClip l_clip = null, float l_volume = 0){
        audioClip = l_clip;
        volume = l_volume;
    }
    public AudioClip audioClip;
    public float volume;
}
#endif