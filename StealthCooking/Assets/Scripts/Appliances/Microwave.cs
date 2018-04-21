using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MicrowaveState { Off, Cooking }

public class Microwave : Appliance
{
    private MicrowaveState state;
    private Food containedItem;
    private List<FoodType> cookableTypes;

    private float cookTime;
    private float cookedInTime;


    private void Update()
    {
        if(state == MicrowaveState.Cooking)
        {
            cookTime -= Time.deltaTime;
            cookedInTime -= Time.deltaTime;

            if(cookedInTime <= 0 && cookableTypes.Contains(containedItem.Type))
            {
                string type = "Cooked" + containedItem.Type;
                containedItem.Type = (FoodType)System.Enum.Parse(typeof(FoodType), type);
            }

            if(cookTime <= 0)
            {
                state = MicrowaveState.Off;
                Debug.Log("Finished cooking");
                //SoundManager.AddSound(15);
            }
        }
    }


    //use for initialization
    private void Start()
    {
        state = MicrowaveState.Off;

        cookableTypes = new List<FoodType>
        {
            FoodType.Pizza
        };
    }


    /// <summary>
    /// Allows the player to interact with the microwave
    /// </summary>
    /// <param name="player"></param>
    public override void Interact(Player player)
    {
        if (player.HeldItem != null && containedItem == null && state == MicrowaveState.Off && cookableTypes.Contains(player.HeldItem.Type))
        {
            containedItem = player.HeldItem;
            player.HeldItem = null;

            state = MicrowaveState.Cooking;

            cookTime = 10;
            cookedInTime = 8;

            Debug.Log("Player left: " + containedItem.Type);
        }
        else if (player.HeldItem == null && containedItem != null && state == MicrowaveState.Off)
        {
            player.HeldItem = containedItem;
            containedItem = null;

            Debug.Log("Player got: " + player.HeldItem.Type);
        }
        else
        {
            //doesn't work
        }
    }
}
