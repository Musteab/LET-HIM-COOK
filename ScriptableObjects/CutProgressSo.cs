using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CutProgressSo : ScriptableObject
{
    public kitchenObjectSo input;
    public kitchenObjectSo output;
    public int cuttingProgressMax;
}
