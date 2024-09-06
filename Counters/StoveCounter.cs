using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static CuttingCounter;
using static IhasProgress;

public class StoveCounter : BaseCounter, IhasProgress
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs: EventArgs
    {
        public State state;
    }

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    [SerializeField] private FryingRecipeSo[] fryingRecipeSoArray;
    [SerializeField] private BurningRecipeSo[] burningRecipeSoArray;

    private State state;
    private float fryingTimer;
    private float burningTimer;
    private FryingRecipeSo fryingRecipeSo;
    private BurningRecipeSo burningRecipeSo;

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

                    OnProgressChanged?.Invoke(this, new IhasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeSo.FryingTimerMax
                    });


                    if (fryingTimer > fryingRecipeSo.FryingTimerMax)
                    {
                        //fried
                        GetKitchenObject().kys();

                        KitchenObject.SpawnKitchenObject(fryingRecipeSo.output, this);
                       
        

                        state = State.Fried;
                        burningTimer = 0f;
                        burningRecipeSo = GetBurningRecipeSoWithInput(GetKitchenObject().GetkitchenObjectSo());

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                            state = state
                        });
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IhasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = burningTimer / burningRecipeSo.burningTimerMax
                    });


                    if (burningTimer > burningRecipeSo.burningTimerMax)
                    {
                        //fried
                        GetKitchenObject().kys();

                        KitchenObject.SpawnKitchenObject(burningRecipeSo.output, this);
                       

                        state = State.Burned;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IhasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });

                    }
                        break;
                case State.Burned:
                    break;
            }
         
        }
    }
    public override void Interact(playerscript player)
    {
        if (!HasKitchenObject())
        {
            //No object in this couunter
            if (player.HasKitchenObject())
            {
                //Player got an object
                if (HasRecipeWithInput(player.GetKitchenObject().GetkitchenObjectSo()))
                {
                    //Can be fried
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                   fryingRecipeSo = GetFryingRecipeSoWithInput(GetKitchenObject().GetkitchenObjectSo());

                    state = State.Frying;
                    fryingTimer = 0f;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });

                    OnProgressChanged?.Invoke(this, new IhasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeSo.FryingTimerMax
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
                //Player got an objectif (player.HasKitchenObject())
                
                    //Player got an object
                    if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    {
                        //Gimme a run for ma money (Player got a plate)
                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetkitchenObjectSo()))
                        {
                            GetKitchenObject().kys();
                            state = State.Idle;
                            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                            {
                                state = state
                            });

                            OnProgressChanged?.Invoke(this, new IhasProgress.OnProgressChangedEventArgs
                            {
                                progressNormalized = 0f
                            });
                        }
                    }
                
            }
            else
            {
                //Player got nothing
                GetKitchenObject().SetKitchenObjectParent(player);

                state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state
                });

                OnProgressChanged?.Invoke(this, new IhasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });

            }
        }
    }
    private bool HasRecipeWithInput(kitchenObjectSo inputKitchenObjectSo)
    {
        FryingRecipeSo fryingRecipeSo = GetFryingRecipeSoWithInput(inputKitchenObjectSo);
        return fryingRecipeSo != null;

    }
    private kitchenObjectSo GetOutputForInput(kitchenObjectSo inputkitchenObjectSo)
    {
        FryingRecipeSo fryingRecipeSo = GetFryingRecipeSoWithInput(inputkitchenObjectSo);
        if (fryingRecipeSo != null)
        {
            return fryingRecipeSo.output;
        }
        else
        {
            return null;
        }
    }
    private FryingRecipeSo GetFryingRecipeSoWithInput(kitchenObjectSo inputKitchenObjectSo)
    {
        foreach (FryingRecipeSo fryingRecipeSo in fryingRecipeSoArray)
        {
            if (fryingRecipeSo.input == inputKitchenObjectSo)
            {
                return fryingRecipeSo;
            }
        }
        return null;
    }
    private BurningRecipeSo GetBurningRecipeSoWithInput(kitchenObjectSo inputKitchenObjectSo)
    {
        foreach (BurningRecipeSo burningRecipeSo in burningRecipeSoArray)
        {
            if (burningRecipeSo.input == inputKitchenObjectSo)
            {
                return burningRecipeSo;
            }
        }
        return null;
    }
    public bool IsFried()
    {
        return state == State.Fried;
    }

}

