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

            if(cookedInTime <= 0)
            {
                containedItem.Type = (FoodType)System.Enum.Parse(typeof(FoodType), "Cooked" + containedItem.Type.ToString());
            }

            if(cookTime <= 0)
            {
                //SoundManager.AddSound(15.f);
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

            cookTime = 30;
            cookedInTime = 26;
        }
        else if (player.HeldItem == null && containedItem != null && state == MicrowaveState.Off)
        {
            player.HeldItem = containedItem;
            containedItem = null;
        }
        else
        {
            //doesn't work
        }
    }
}
