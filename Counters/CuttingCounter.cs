using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class CuttingCounter : BaseCounter, IhasProgress
{
    public static event EventHandler OnAnyCut;
    public event EventHandler<IhasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;

    [SerializeField] private CutProgressSo[] cutprogressSoArray;

    private int cuttingProgress;
    public override void Interact(playerscript player)
    {
        if (!HasKitchenObject())
        {
            //No object in this couunter
            if (player.HasKitchenObject())
            {
                //Player got an object
              if(HasRecipeWithInput(player.GetKitchenObject().GetkitchenObjectSo()))
                { 
                    //Can be cut
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;

                    CutProgressSo cutProgressSo = GetCuttingRecipeSsoWithInput(GetKitchenObject().GetkitchenObjectSo());

                    OnProgressChanged?.Invoke(this, new IhasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)cuttingProgress / cutProgressSo.cuttingProgressMax
                    });
                }
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
                if (player.HasKitchenObject())
                {
                    //Player got an object
                    if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    {
                        //Gimme a run for ma money (Player got a plate)
                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetkitchenObjectSo()))
                        {
                            GetKitchenObject().kys();
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

    public override void InteractAlternate(playerscript player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetkitchenObjectSo()))
        {
            //Cutty cutty you naughty naughty
            cuttingProgress++;

            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);

            CutProgressSo cutProgressSo = GetCuttingRecipeSsoWithInput(GetKitchenObject().GetkitchenObjectSo());

            OnProgressChanged?.Invoke(this, new IhasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / cutProgressSo.cuttingProgressMax
            });

            if (cuttingProgress >= cutProgressSo.cuttingProgressMax)
            {
                kitchenObjectSo outputKitchenObjectSo = GetOutputForInput(GetKitchenObject().GetkitchenObjectSo());

                GetKitchenObject().kys();


                KitchenObject.SpawnKitchenObject(outputKitchenObjectSo, this);
            }
        }

    }
    private bool HasRecipeWithInput(kitchenObjectSo inputKitchenObjectSo)
    {
        CutProgressSo cutProgressSo = GetCuttingRecipeSsoWithInput(inputKitchenObjectSo);
        return cutProgressSo != null;
       
    }
    private kitchenObjectSo GetOutputForInput(kitchenObjectSo inputkitchenObjectSo)
    {
        CutProgressSo cutProgressSo = GetCuttingRecipeSsoWithInput(inputkitchenObjectSo);
        if (cutProgressSo != null)
        {
            return cutProgressSo.output;
        }
        else
        {
            return null;
        }
    }
    private CutProgressSo GetCuttingRecipeSsoWithInput(kitchenObjectSo inputKitchenObjectSo)
    {
        foreach (CutProgressSo cutProgressSo in cutprogressSoArray)
        {
            if (cutProgressSo.input == inputKitchenObjectSo)
            {
                return cutProgressSo;
            }
        }
        return null;
    }

}
