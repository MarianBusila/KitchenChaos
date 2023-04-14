using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<OnStateChangeEventArgs> OnStateChange;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public class OnStateChangeEventArgs : EventArgs
    {
        public State state;
    }
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private State state;
    private float fryingTimer;
    private float burningTimer;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;

    private void Start()
    {
        state = State.Idle;
    }
    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = (float)fryingTimer / fryingRecipeSO.fryingTimerMax });
                    if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                    {
                        // Fried
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                        
                        state = State.Fried;
                        burningTimer = 0f;
                        burningRecipeSO = GetBurningRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());
                        OnStateChange?.Invoke(this, new OnStateChangeEventArgs { state = state });
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = (float)burningTimer / burningRecipeSO.burningTimerMax });
                    if (burningTimer > burningRecipeSO.burningTimerMax)
                    {
                        // Burned
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
                        state = State.Burned;
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0f });
                        OnStateChange?.Invoke(this, new OnStateChangeEventArgs { state = state });
                    }
                    break;
                case State.Burned:
                    break;
            }
        }                    
        
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //there is no KitchenObject here
            if (player.HasKitchenObject())
            {
                // Player is carrying something - put it on the counter
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    // Player is carrying something that can be fried
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    fryingRecipeSO = GetFryinRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());
                    state = State.Frying;
                    fryingTimer = 0;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = (float)fryingTimer / fryingRecipeSO.fryingTimerMax });
                    OnStateChange?.Invoke(this, new OnStateChangeEventArgs { state = state });
                }
            }
            else
            {
                // Player not carrying anything
            }
        }
        else
        {
            //there is KitchenObject here
            if (player.HasKitchenObject())
            {
                // Player is carrying something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // Player is carring a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();

                        state = State.Idle;
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0 });
                        OnStateChange?.Invoke(this, new OnStateChangeEventArgs { state = state });
                    }
                }
            }
            else
            {
                // Player not carrying anything - give it to player
                GetKitchenObject().SetKitchenObjectParent(player);

                state = State.Idle;
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0 });
                OnStateChange?.Invoke(this, new OnStateChangeEventArgs { state = state });
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryinRecipeSOFromInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputFromInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryinRecipeSOFromInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipeSO GetFryinRecipeSOFromInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
                return fryingRecipeSO;
        }

        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOFromInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
                return burningRecipeSO;
        }

        return null;
    }

    public bool IsFried()
    {
        return state == State.Fried;
    }
}
