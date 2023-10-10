using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour {


    public event EventHandler OnRecipeSpawn;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeListSO;
    

    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;
    private int successfulRecipesAmount; 

    private void Awake() {
        Instance = this;

        waitingRecipeSOList = new List<RecipeSO>();
    }
    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f) {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (KitchenGameManager.Instance.IsGamePlaying() && waitingRecipeSOList.Count < waitingRecipeMax) {
                RecipeSO waitingRecipeSO = recipeListSO.reicpeSOList[UnityEngine.Random.Range(0, recipeListSO.reicpeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawn?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipie(PlateKitchenObject plateKitchenObject) {
        for (int i = 0; i < waitingRecipeSOList.Count; ++i) {
            RecipeSO waitingRecpieSO = waitingRecipeSOList[i];

            if (waitingRecpieSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjSOList().Count) {
                //Has the same number of ingridents
                bool plateContentsMatchesRecipe = true;
                foreach (KitchenObjSO recipeKitchenObjectSO in waitingRecpieSO.kitchenObjectSOList) {
                    //Cyclig through all ingredients in recipie

                    bool ingredientFound = false;
                    foreach (KitchenObjSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjSOList()) {
                        //Cycling through all ingredients in plate
                        if (plateKitchenObjectSO == recipeKitchenObjectSO) {
                            //Ingredient matches
                            ingredientFound = true;
                            break;
                        }
                    
                    }
                    if (!ingredientFound) {
                        //This recipie ingredient was not found on the Plate
                        plateContentsMatchesRecipe = false;
                    }
                }
                if (plateContentsMatchesRecipe) {
                    // Player deliver correct recipe
                    successfulRecipesAmount++;

                    waitingRecipeSOList.RemoveAt(i);

                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                
                }
            }
        }
        //No matches found
        //Player did not deliver correct recipe
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList() { 
        return waitingRecipeSOList;
    }

    public int GetSuccessfulRecipesAmount() {
        return successfulRecipesAmount;
    }
}
