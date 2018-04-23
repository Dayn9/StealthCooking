using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : Appliance
{
    [SerializeField] private AudioSource source;

    [SerializeField] private FoodType startingType;
    private Food storedItem;
    private Dictionary<FoodType, Dictionary<FoodType, FoodType>> foodCombinations;

    [SerializeField] private RecipieManager recipie;

    public Food StoredItem { get { return storedItem; } }

    /// <summary>
    /// Sets up possible combinations
    /// </summary>
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
        AddCombination(FoodType.Tortilla, FoodType.Rice, FoodType.RicedTortilla);
        AddCombination(FoodType.RicedTortilla, FoodType.Beans, FoodType.BeanAndRiceTortilla);
        AddCombination(FoodType.BeanAndRiceTortilla, FoodType.GroundBeef, FoodType.Burrito);
        AddCombination(FoodType.Bread, FoodType.Cheese, FoodType.GrilledCheese);
        AddCombination(FoodType.Noodles, FoodType.Water, FoodType.WetNoodles);
        AddCombination(FoodType.WetNoodles, FoodType.Sauce, FoodType.Spaghetti);
        AddCombination(FoodType.Chicken, FoodType.Cheese, FoodType.CheesedChicken);
        AddCombination(FoodType.CheesedChicken, FoodType.Ham, FoodType.ChickenCordonBleu);
    }
    /// <summary>
    /// Sets up items starting on the table
    /// </summary>
    private void Start()
    {
        if (startingType != FoodType.Null)
        {
            storedItem = gameObject.AddComponent<Food>();
            storedItem.Type = startingType;
        }
    }

    /// <summary>
    /// Adds a possible combination to the list
    /// </summary>
    /// <param name="reactant_1">First item in combination</param>
    /// <param name="reactant_2">Second item in combination</param>
    /// <param name="product">Product of the two items</param>
    private void AddCombination(FoodType reactant_1, FoodType reactant_2, FoodType product)
    {
        foodCombinations[reactant_1].Add(reactant_2, product);
        foodCombinations[reactant_2].Add(reactant_1, product);
    }

    /// <summary>
    /// Lets the player interact with the crafting table
    /// </summary>
    public override void Interact(Player player)
    {
        //player gets item from table
        if(player.HeldItem == null && storedItem != null)
        {
            player.HeldItem = storedItem;
            storedItem = null;

            SoundManager.AddSound(2, transform.position, SoundManager.pickup, source);

            Debug.Log("Player got: " + player.HeldItem.Type);
        }
        //player puts item on empty table
        else if(storedItem == null && player.HeldItem != null)
        {
            storedItem = player.HeldItem;
            player.HeldItem = null;

            SoundManager.AddSound(2, transform.position, SoundManager.place, source);

            Debug.Log("Player left: " + storedItem.Type);

            recipie.UpdateList(storedItem.Type);
        }
        //combines items
        else if (storedItem != null && player.HeldItem != null)
        {
            if (foodCombinations[player.HeldItem.Type].ContainsKey(storedItem.Type))
            {
                storedItem.Type = foodCombinations[player.HeldItem.Type][storedItem.Type];
                player.HeldItem = null;

                //SoundManager.AddSound(4, transform.position, SoundManager.squish, source);

                recipie.UpdateList(storedItem.Type);
            }

            Debug.Log("Table now has: " + storedItem.Type);
        }
        else
        {
            //doesn't work
        }
    }
}
