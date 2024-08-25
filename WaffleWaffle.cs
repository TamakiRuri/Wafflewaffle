
using System;
using UdonSharp;
using UnityEditor;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

public class WaffleWaffle : UdonSharpBehaviour
{
    [SerializeField]private AudioClip[] audioClips;
    [SerializeField]private float[] volumes;
    [SerializeField]private AudioSource targetAudioSource;
    [UdonSynced] private int audioPlay;
    void Start()
    {
        if (targetAudioSource == null) targetAudioSource=gameObject.GetComponent<AudioSource>();
    }

    public override void Interact(){
        if (!Networking.IsOwner(Networking.LocalPlayer, gameObject)){
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
        }
    }

    public override void OnPickupUseDown()
    {
        audioPlay =  UnityEngine.Random.Range(0, audioClips.Length);
        SendCustomNetworkEvent(NetworkEventTarget.All, "SoundPlay");
    }

    public void SoundPlay(){
        if (audioClips.Length != 0 && volumes.Length != 0 && !targetAudioSource.isPlaying){
            targetAudioSource.clip = audioClips[audioPlay];
            if (volumes.Length == 1){
                targetAudioSource.volume = volumes[0];
                targetAudioSource.Play();
            }
            else if (audioClips.Length == volumes.Length){
                targetAudioSource.volume = volumes[audioPlay];
                targetAudioSource.Play();
            }
        }
    }

#if UNITY_EDITOR
    public void ImportAudio(AudioClip[] l_audio){
        audioClips = l_audio;
    }

    public void ImportVolume(float[] l_volume){
        volumes = l_volume;
    }

#endif

}