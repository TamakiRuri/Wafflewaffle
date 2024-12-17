
using System;
using System.Collections;
using UdonSharp;
using UnityEditor;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class WaffleWaffleManual : UdonSharpBehaviour
{
    [SerializeField]protected AudioClip[] audioClips;
    [SerializeField]protected float[] volumes;
    [SerializeField]protected bool[] doEats;
    [SerializeField]protected ParticleSystem eatParticle;
    [SerializeField]protected AudioSource targetAudioSource;
    [SerializeField]protected bool allowInterruption;
    [UdonSynced] protected int audioPlay;
    void Start()
    {
        if (targetAudioSource == null) targetAudioSource=gameObject.GetComponent<AudioSource>();
    }

    public void SoundEvent(){
        Debug.Log("Waffle: Received Interaction");
        if (!Networking.IsOwner(Networking.LocalPlayer, gameObject)){
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
        }
        audioPlay =  UnityEngine.Random.Range(0, audioClips.Length);
        RequestSerialization();
        SoundPlay();
    }
    public override void OnDeserialization()
    {
        SoundPlay();
    }
    public void SoundPlay(){
        bool interruptionCheck = !targetAudioSource.isPlaying || allowInterruption;
        if (audioClips.Length != 0 && volumes.Length != 0 && interruptionCheck){
            targetAudioSource.clip = audioClips[audioPlay];
            if (volumes.Length == 1){
                targetAudioSource.volume = volumes[0];
                targetAudioSource.Play();
                EmitEatParticle(0);
            }
            else if (audioClips.Length == volumes.Length){
                targetAudioSource.volume = volumes[audioPlay];
                targetAudioSource.Play();
                EmitEatParticle(audioPlay);
            }
        }
    }
    protected void EmitEatParticle(int l_index){
        if(doEats[l_index] && eatParticle != null){
            eatParticle.Play();
        }
    }

#if UNITY_EDITOR && !COMPILER_UDON
    public void ImportDatas(AudioClip[] l_audio, float[] l_volume, bool[] l_doEat, bool l_allowInterruption){
        audioClips = l_audio;
        volumes = l_volume;
        doEats = l_doEat;
        allowInterruption = l_allowInterruption;
    }

#endif

}