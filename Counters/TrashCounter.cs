using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnobjectTrashed;
    public override void Interact(playerscript player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().kys();

            OnobjectTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
}
