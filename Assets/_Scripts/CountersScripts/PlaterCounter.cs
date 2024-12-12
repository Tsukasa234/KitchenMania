using System;
using UnityEngine;

public class PlaterCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;

    [SerializeField] private KitchenObject_SO plateKitchenObjectSO;

    [SerializeField] private float plateSpawnTimerMax = 4f;
    private float spawnPlateTimer;

    private int spawnPlateAmount;
    private int spawnPlateAmountMax = 4;

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > plateSpawnTimerMax)
        {
            spawnPlateTimer = 0;
            if (spawnPlateAmount < spawnPlateAmountMax)
            {
                spawnPlateAmount++;

                OnPlateSpawned(this, EventArgs.Empty);
            }     
        }
    }
}
