using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateRemoved;
    public event EventHandler OnPlateSpawned;

    [SerializeField] private kitchenObjectSo plateKitchenObjectSo;

    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int plateSpawnedAmount;
    private int plateSpawnedAmountMax = 4;

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if(spawnPlateTimer > spawnPlateTimerMax )
        {
            spawnPlateTimer = 0f;

            if (plateSpawnedAmount < plateSpawnedAmountMax)
            {
                plateSpawnedAmount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(playerscript player)
    {
        if (!player.HasKitchenObject())
        {
            //Has nothing
            if (plateSpawnedAmount > 0)
            {
                //There's at least a plate
                plateSpawnedAmount--;

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSo, player);

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }


}
