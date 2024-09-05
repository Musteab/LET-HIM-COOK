using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeSo : ScriptableObject
{
    public kitchenObjectSo input;
    public kitchenObjectSo output;
    public float FryingTimerMax;
}
