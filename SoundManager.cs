using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREF_SOUND_EFFECT_VOLUME = "SoundEffectVolume";

    public static SoundManager instance;
    [SerializeField] private AudioRefScriptableObject audioRefScriptableObject;

    private float volume = 1f;
    private void Awake()
    {
        instance = this;

       volume = PlayerPrefs.GetFloat(PLAYER_PREF_SOUND_EFFECT_VOLUME, 1f);
    }
    private void Start()
    {
        DeliveryManager.Instance.OnDeliverySucess += DeliveryManager_OnDeliverySucess;
        DeliveryManager.Instance.OnDeliveryfailed += DeliveryManager_OnDeliveryfailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        playerscript.Instance.OnPickupSomething += player_OnPickupSomething;
        BaseCounter.Onobjectdropped += BaseCounter_Onobjectdropped;
        TrashCounter.OnobjectTrashed += TrashCounter_OnobjectTrashed;
    }

    private void TrashCounter_OnobjectTrashed(object sender, System.EventArgs e)
    {
        PlaySound(audioRefScriptableObject.trash, Camera.main.transform.position);
    }

    private void BaseCounter_Onobjectdropped(object sender, System.EventArgs e)
    {
        PlaySound(audioRefScriptableObject.objectDrop, Camera.main.transform.position);
    }

    private void player_OnPickupSomething(object sender, System.EventArgs e)
    {
        PlaySound(audioRefScriptableObject.objectPickup, Camera.main.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        PlaySound(audioRefScriptableObject.chop, Camera.main.transform.position);
    }

    private void DeliveryManager_OnDeliveryfailed(object sender, System.EventArgs e)
    {
        PlaySound(audioRefScriptableObject.DeliveryFailed, Camera.main.transform.position);
    }

    private void DeliveryManager_OnDeliverySucess(object sender, System.EventArgs e)
    {
        PlaySound(audioRefScriptableObject.DeliverySuccess, Camera.main.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }
    private void PlaySound (AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint (audioClip, position, volume);
    }

    public void PlayFootstepSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioRefScriptableObject.footstep, position, volumeMultiplier * volume);
    }
    public void PlayCountDownSound()
    {
        PlaySound(audioRefScriptableObject.warning, Camera.main.transform.position);
    }
    public void PlayWarningSound(Vector3 postion)
    {
        PlaySound(audioRefScriptableObject.warning, postion);
    }
    public void ChangeVolumne()
    {
        volume += .1f;
        if (volume > 1f)
        {
            volume = 0f;
        }

        PlayerPrefs.SetFloat(PLAYER_PREF_SOUND_EFFECT_VOLUME, volume);
        PlayerPrefs.Save();
    }
    public float GetVolume()
    {
        return volume;
    }
}
