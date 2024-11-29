
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class WaffleTrigger : UdonSharpBehaviour
{
    [SerializeField] private UdonBehaviour waffle;
    void Start()
    {
        if (waffle == null){
            waffle = (UdonBehaviour)gameObject.GetComponentInChildren(typeof(UdonBehaviour));
        }
    }
    public override void Interact(){
        if (!Networking.IsOwner(Networking.LocalPlayer, gameObject)){
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
        }
    }

    public override void OnPickupUseDown()
    {
        waffle.SendCustomEvent("SoundEvent");
    }
}
