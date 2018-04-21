using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : Appliance
{
    [SerializeField] private Food storedItem;
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
        AddCombination(FoodType.SaucedDough, FoodType.Cheese, FoodType.UncookedPizza);
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
        }
        else if(storedItem == null && player.HeldItem != null)
        {
            storedItem = player.HeldItem;
            player.HeldItem = null;
        }
        else if (storedItem != null && player.HeldItem != null)
        {
            //test for combination
            
        }
    }
}
