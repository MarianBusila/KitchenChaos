using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public override void Interact(Player player)
    {
        if(player.GetKitchenObject())
        {
            if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                // only accept plates
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                KitchenObject.DestroyKitchenObject(player.GetKitchenObject());
            }
        }
    }
}
