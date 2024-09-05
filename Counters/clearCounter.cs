using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearCounter : BaseCounter
{
    [SerializeField] private kitchenObjectSo kitchenObjectSo;

    public override void Interact(playerscript player)
    {
        if (!HasKitchenObject())
        {
            //No object in this couunter
            if (player.HasKitchenObject())
            {
                //Player got an object
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //Player got nothing
            }
        }
        else
        {
            //There is an object in this counter
            if (player.HasKitchenObject())
            {
                //Player got an object
                if (player.GetKitchenObject().TryGetPlate (out PlateKitchenObject plateKitchenObject))          
                {
                    //Gimme a run for ma money (Player got a plate)
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetkitchenObjectSo()))
                    {
                        GetKitchenObject().kys();
                    }    
                }
                else
                {
                    //No plate player is carrying something else
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        //He gotta a plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetkitchenObjectSo()))
                        {
                            player.GetKitchenObject().kys();
                        }
                    }
                }
            }
            else
            {
                //Player got nothing
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

}
