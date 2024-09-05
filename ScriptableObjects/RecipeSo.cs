using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecipeSo : ScriptableObject
{
    public List<kitchenObjectSo> kitchenObjectSoList;
    public string recipeName;
}
