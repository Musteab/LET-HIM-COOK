using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct kitchenObjectSo_GameObject
    {
        public kitchenObjectSo kitchenObjectSo;
        public GameObject gameObject;
    }


    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<kitchenObjectSo_GameObject> kitchenObjectSo_GameObjectList;

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        foreach (kitchenObjectSo_GameObject kitchenObjectSoGameObject in kitchenObjectSo_GameObjectList)
        {
            kitchenObjectSoGameObject.gameObject.SetActive(false);
            
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
      foreach(kitchenObjectSo_GameObject kitchenObjectSoGameObject in kitchenObjectSo_GameObjectList)
        {
            if(kitchenObjectSoGameObject.kitchenObjectSo == e.kitchenObjectSo)
            {
                kitchenObjectSoGameObject.gameObject.SetActive(true);
            }
        }
    }
}
