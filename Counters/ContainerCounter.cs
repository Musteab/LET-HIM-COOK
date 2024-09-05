using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPLayerGrabbedObject;

    [SerializeField] private kitchenObjectSo kitchenObjectSo;

    public override void Interact(playerscript player)
    {
        if (!player.HasKitchenObject())
        {
            //Player got an object
            KitchenObject.SpawnKitchenObject(kitchenObjectSo, player);
            OnPLayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
    
    
}
