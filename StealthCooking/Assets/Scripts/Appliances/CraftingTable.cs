using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : Appliance
{
    [SerializeField] private FoodType startingType;
    private Food storedItem;
    private Dictionary<FoodType, Dictionary<FoodType, FoodType>> foodCombinations;



    void Awake()
    {
        // initialization
        foodCombinations = new Dictionary<FoodType, Dictionary<FoodType, FoodType>>();
        foreach (FoodType value in Enum.GetValues(typeof(FoodType)))
        {
            foodCombinations.Add(value, new Dictionary<FoodType, FoodType>());
        }
        //Set up combinations here
        AddCombination(FoodType.Dough, FoodType.Sauce, FoodType.SaucedDough);
        AddCombination(FoodType.SaucedDough, FoodType.Cheese, FoodType.Pizza);
    }

    private void Start()
    {
        if (startingType != FoodType.Null)
        {
            storedItem = gameObject.AddComponent<Food>();
            storedItem.Type = startingType;
        }
    }


    private void AddCombination(FoodType reactant_1, FoodType reactant_2, FoodType product)
    {
        foodCombinations[reactant_1].Add(reactant_2, product);
        foodCombinations[reactant_2].Add(reactant_1, product);
    }

    public override void Interact(Player player)
    {
        if(player.HeldItem == null && storedItem != null)
        {
            player.HeldItem = storedItem;
            storedItem = null;

            Debug.Log("Player got: " + player.HeldItem.Type);
        }
        else if(storedItem == null && player.HeldItem != null)
        {
            storedItem = player.HeldItem;
            player.HeldItem = null;

            Debug.Log("Player left: " + storedItem.Type);
        }
        else if (storedItem != null && player.HeldItem != null)
        {
            if (foodCombinations[player.HeldItem.Type].ContainsKey(storedItem.Type))
            {
                storedItem.Type = foodCombinations[player.HeldItem.Type][storedItem.Type];
                player.HeldItem = null;
            }

            Debug.Log("Table now has: " + storedItem.Type);
        }
    }
}
