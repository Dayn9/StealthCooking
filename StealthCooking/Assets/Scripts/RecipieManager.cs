using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipieManager : MonoBehaviour
{
    public static bool eaten;

    //                 Food Step             Object  Whether it's been checked off or not
    private Dictionary<FoodType, KeyValuePair<Text, bool>> toDoList;
    [SerializeField] private List<Text> toDoListTextObjects = new List<Text>();


    void Awake()
    {
        eaten = false;

        toDoList = new Dictionary<FoodType, KeyValuePair<Text, bool>>();
        toDoList.Add(FoodType.Dough, new KeyValuePair<Text, bool>(toDoListTextObjects[0], false));
        toDoList.Add(FoodType.SaucedDough, new KeyValuePair<Text, bool>(toDoListTextObjects[1], false));
        toDoList.Add(FoodType.Pizza, new KeyValuePair<Text, bool>(toDoListTextObjects[2], false));
        toDoList.Add(FoodType.CookedPizza, new KeyValuePair<Text, bool>(toDoListTextObjects[3], false));

    }

    

    /// <summary>
    /// Updates the checklist with the food that was just created
    /// </summary>
    /// <param name="typeOfFoodCreated"></param>
    public void UpdateList(FoodType typeOfFoodCreated)
    {
        foreach(KeyValuePair<FoodType, KeyValuePair<Text, bool>> entry in toDoList)
        {
            if (entry.Key == typeOfFoodCreated && !entry.Value.Value)
            {
                entry.Value.Key.color = Color.green;
                entry.Value.Key.text = "  [X] " + entry.Value.Key.text.Substring(7);
            }
        }

        bool done = true;
        foreach (KeyValuePair<FoodType, KeyValuePair<Text, bool>> entry in toDoList)
        {
            if(entry.Value.Key.color != Color.green)
            {
                done = false;
            }
        }

        if (done) { eaten = true; }
    }
}
