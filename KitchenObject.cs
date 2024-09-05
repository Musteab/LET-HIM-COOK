using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private kitchenObjectSo kitchenObjectSo;

    private IKitchenObjectParents kitchenObjectParent;

    public kitchenObjectSo GetkitchenObjectSo() 
    {
        return kitchenObjectSo;
    }

    public void SetKitchenObjectParent(IKitchenObjectParents kitchenObjectParent)
    {
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        this.kitchenObjectParent = kitchenObjectParent;

        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("IKitchenObjectParents already has a kitcheObject");
        }

        kitchenObjectParent.SetKitchenObject(this);

        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
    public IKitchenObjectParents GetKitchenObjectParent()
    {
      return kitchenObjectParent;
    }

    public void kys()
    {
        kitchenObjectParent.ClearKitchenObject();

        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject=null;
            return false;
        }
    }

    public static KitchenObject SpawnKitchenObject(kitchenObjectSo kitchenObjectSo, IKitchenObjectParents kitchenObjectParents)
    {
        Transform KitchenObectTransform = Instantiate(kitchenObjectSo.prefab);
        KitchenObject kitchenObject = KitchenObectTransform.GetComponent<KitchenObject>();
        
        kitchenObject.SetKitchenObjectParent(kitchenObjectParents);

        return kitchenObject;
    }

}
