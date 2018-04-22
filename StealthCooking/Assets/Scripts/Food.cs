using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodType
{
    Dough,
    Sauce,
    Cheese,
    Pizza,
    SaucedDough,
    CookedPizza,

    //etc



    Null //only use this when setting the default food in an appliance to be "null"
}

public class Food : MonoBehaviour
{
    [SerializeField] private FoodType type;

    public FoodType Type { get { return type; } set { type = value; } }
}
