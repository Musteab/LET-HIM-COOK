using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs: EventArgs
    {
        public kitchenObjectSo kitchenObjectSo;
    }

    [SerializeField] private List<kitchenObjectSo> validKitchenObjectSoList;

    private List<kitchenObjectSo> kitchenObjectSoList;


    private void Awake()
    {
        kitchenObjectSoList = new List<kitchenObjectSo>();
    }
    public bool TryAddIngredient(kitchenObjectSo kitchenObjectSo)
    {
        if (!validKitchenObjectSoList.Contains(kitchenObjectSo))
        {
            //Brotha we dont sell full tomatos
            return false;
        }
        if (kitchenObjectSoList.Contains(kitchenObjectSo))
        {
            //Nuh uh
            return false;
        }
        else
        {
            kitchenObjectSoList.Add(kitchenObjectSo);

            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                kitchenObjectSo = kitchenObjectSo
            });

            return true;
        }   
    }

    public List<kitchenObjectSo> GetKitchenObjectSoList()
    {
        return kitchenObjectSoList;
    }
    
}
