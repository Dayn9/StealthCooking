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
    UncookedPizza,
    CookedPizza

    //etc
}

public class Food : MonoBehaviour
{
    [SerializeField] private FoodType type;

    public FoodType Type { get { return type; } set { type = value; } }



	// Use this for initialization
	//void Start ()
 //   {
		
	//}
	
	// Update is called once per frame
	//void Update ()
 //   {
		
	//}
}
