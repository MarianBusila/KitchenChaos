using System.Collections;
using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            // give the player a new kitchen object
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
