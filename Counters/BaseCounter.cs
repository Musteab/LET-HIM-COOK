using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParents
{
    public static void ResetStaticData()
    {
        Onobjectdropped = null;
    }


    [SerializeField] private Transform counterTopoint;

    public static event EventHandler Onobjectdropped;
    private KitchenObject kitchenObject;
    public virtual void Interact(playerscript player)
    {

    }

    public virtual void InteractAlternate(playerscript player)
    {

    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null)
        {
            Onobjectdropped?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {

        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

}
