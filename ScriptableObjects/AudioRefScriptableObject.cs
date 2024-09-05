using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioRefScriptableObject : ScriptableObject
{
    public AudioClip[] chop;
    public AudioClip[] DeliveryFailed;
    public AudioClip[] DeliverySuccess;
    public AudioClip[] footstep;
    public AudioClip[] objectDrop;
    public AudioClip[] objectPickup;
    public AudioClip[] stovesizzle;
    public AudioClip[] trash;
    public AudioClip[] warning;

}
