using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DeliveryManager : MonoBehaviour
{

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnDeliverySucess;
    public event EventHandler OnDeliveryfailed;
    public static DeliveryManager Instance {  get; private set; }

    [SerializeField] private RecipesListSo recipesListSo;

    private List<RecipeSo> waitingRecipeSoList;
    private float spawnRecipeeTimer;
    private float spawnTimerMax = 4f;
    private int waintingRecipesMax = 4;
    private int successfulRecipesAmount;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSoList = new List<RecipeSo>();
    }

    private void Update()
    {
        spawnRecipeeTimer -= Time.deltaTime;
        if(spawnRecipeeTimer <= 0f)
        {
            spawnRecipeeTimer = spawnTimerMax;

            if (waitingRecipeSoList.Count < waintingRecipesMax)
            {
                RecipeSo waitingRecipeSo = recipesListSo.recipeSoList[UnityEngine.Random.Range(0, recipesListSo.recipeSoList.Count)];
               
                waitingRecipeSoList.Add(waitingRecipeSo);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSoList.Count; ++i)
        {
            RecipeSo waitingRecipeSo = waitingRecipeSoList[i];

            if(waitingRecipeSo.kitchenObjectSoList.Count == plateKitchenObject.GetKitchenObjectSoList().Count)
            {
                //hAS THE SAME NUMBER
                bool plateContentMatchesRecipe = true;
                foreach (kitchenObjectSo recipeKitchenSObjectSo in waitingRecipeSo.kitchenObjectSoList)
                {
                    //Loop through all ingredients in this recipe
                    bool ingredientFound = false;
                    foreach (kitchenObjectSo plateKitchenObjectSo in plateKitchenObject.GetKitchenObjectSoList())
                    {
                        //Loop through all ingredients in the plate
                        if (plateKitchenObjectSo == recipeKitchenSObjectSo)
                        {
                            //Found the ingredient
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        //The ingredient for this recipe aint here
                        plateContentMatchesRecipe = false;
                    }
                }
                if (plateContentMatchesRecipe)
                {
                    //Playerdelivered the recipe correctly
                    successfulRecipesAmount++;
                    waitingRecipeSoList.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnDeliverySucess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }  
        }
        //No matching items
        //Playergot the recipe wrong
        OnDeliveryfailed?.Invoke(this, EventArgs.Empty);
        
    }

    public List<RecipeSo> GetWaitingRecipeSoList()
    {
        return waitingRecipeSoList;
    }

    public int GetSuccesfulRecipesAmount()
    {
        return successfulRecipesAmount;
    }

}
