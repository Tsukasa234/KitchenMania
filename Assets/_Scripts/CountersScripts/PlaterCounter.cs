using System;
using UnityEngine;

public class PlaterCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

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
            if (GameManager.Instance.IsGamePlaying() && spawnPlateAmount < spawnPlateAmountMax)
            {
                spawnPlateAmount++;

                OnPlateSpawned(this, EventArgs.Empty);
            }     
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            //Player is Empty Handed
            if (spawnPlateAmount > 0)
            {
                spawnPlateAmount--;

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
