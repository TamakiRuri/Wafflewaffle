#if UNITY_EDITOR && !COMPILER_UDONSHARP
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
//using VRC.Udon;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class WaffleImport : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;
    [SerializeField]private VisualTreeAsset audioListTemplate;

    [MenuItem("Tools/Studio Saphir/Waffle Import")]
    public static void ShowExample()
    {
        WaffleImport wnd = GetWindow<WaffleImport>();
        wnd.titleContent = new GUIContent("Waffle Import");
    }
    List<AudioList> audioLists=new List<AudioList>();
    List<AudioClip> F_audioClips = new List<AudioClip>();
    List<float> F_volume = new List<float>();
    // UdonBehaviour wafflePlayer;
    ImportAudioDatabase wafflePlayer;

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("Import");
        root.Add(label);

        // Instantiate UXML
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);


        //Add items
        var targetList = root.Q<ListView>("audio-list");
        targetList.makeItem = audioListTemplate.CloneTree;
        targetList.bindItem = (e,i)=>{};
        targetList.itemsSource = audioLists;

        SetupButtonHandler();
    }
    [Serializable]private struct AudioList{
        public AudioList(AudioClip l_clip=null, float l_volume = 0){
            targetClip = l_clip;
            targetVolume = l_volume;
        }
        public AudioClip targetClip;
        public float targetVolume;
    }
    private void SetupButtonHandler(){
        VisualElement root = rootVisualElement;

        var buttons = root.Query<Button>();
        buttons.ForEach(RegisterHandler);
    }
    private void RegisterHandler(Button button){
        if (button.name == "generate-data")
            button.RegisterCallback<ClickEvent>(GenerateWaffleData);
        if (button.name == "reload-data"){
            button.RegisterCallback<ClickEvent>(ReloadWaffleData);
        }
    }
    private void AudioClipHandler(VisualElement l_obj){
        AudioClip l_clip = (AudioClip)l_obj.Q<ObjectField>().value;
        float l_volume = l_obj.Q<FloatField>().value;
        if (l_volume < 0 || l_volume > 1 || l_clip == null){
            VisualElement root = rootVisualElement;
            root.Q<Label>("result").text = "Clip can't be null, and Volume should be >= 0 and <= 1";
            throw new ArgumentOutOfRangeException("Clip can't be null, and Volume should be >= 0 and <= 1");
        }
        F_audioClips.Add(l_clip);
        F_volume.Add(l_volume);
    }
    private void GenerateWaffleData(ClickEvent _event){
        VisualElement root = rootVisualElement;
        root.Q<Label>("result").text = "Wait";
        wafflePlayer = ((GameObject)root.Q<ObjectField>("waffle-player").value).GetComponent<ImportAudioDatabase>();
        if (wafflePlayer == null) {
            root.Q<Label>("result").text = "Please Select a Waffle Player";
            return;
        }
        F_audioClips.Clear();
        F_volume.Clear();

        try {
        root.Query<VisualElement>("iltemplate").ForEach(AudioClipHandler);
        }
        catch (Exception e){
            root.Q<Label>("result").text = e.ToString();
            throw e;
        }
        generateDatatoWaffle();
        root.Q<Label>("result").text = "Finished";
        Debug.Log("Generate Finished. \"Should Run Behavior\" Errors are safe to ignore.");
    }
    private void ReloadWaffleData(ClickEvent _event){
        VisualElement root = rootVisualElement;
        root.Q<Label>("result").text = "Wait";
        wafflePlayer = ((GameObject)root.Q<ObjectField>("waffle-player").value).GetComponent<ImportAudioDatabase>();
        Debug.Log("Started Loading data.");
        if (wafflePlayer == null) 
        {
            root.Q<Label>("result").text = "Please select a waffle player";
            return;
        }
        AudioClip[] l_clips = wafflePlayer.exportAudioData();
        float[] l_volume = wafflePlayer.exportVolumeData();
        //Debug.Log("Started Loading data.");
        AudioList[] l_list = new AudioList[l_clips.Length];
        for (int i = 0; i < l_clips.Length; i++){
            l_list[i] = new AudioList(l_clips[i], l_volume[i]);
        }
        audioLists = l_list.ToList();
        foreach (AudioList ll_list in audioLists){
            Debug.Log("Clip " + ll_list.targetClip + " Volume " + ll_list.targetVolume + " Loaded");
        }
        //add items
        var targetList = root.Q<ListView>("audio-list");
        targetList.itemsSource = audioLists;
        targetList.bindItem = (e,i)=>{
            (e.ElementAt(0).ElementAt(0) as ObjectField).value = audioLists[i].targetClip;
            (e.ElementAt(0).ElementAt(1) as FloatField).value = audioLists[i].targetVolume;
        };
        root.Q<Label>("result").text = "Finished";
    }
    private void generateDatatoWaffle(){
        AudioClip[] l_audios = F_audioClips.ToArray();
        float[] l_volume = F_volume.ToArray();
        wafflePlayer.importAudioData(l_audios, l_volume);
        wafflePlayer.generateDatatoWaffle();
    }
    
}
#endif