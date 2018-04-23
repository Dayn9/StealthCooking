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

    private AudioSource audioSource;

    [SerializeField] private RecipieManager recipie;

    //update runs every frame
    private void Update()
    {
        if(state == MicrowaveState.Cooking)
        {
            SoundManager.AddSoundContinuous(1f, transform.position, SoundManager.microwaveHum, audioSource, 60);

            cookTime -= Time.deltaTime;
            cookedInTime -= Time.deltaTime;

            if(cookedInTime <= 0 && cookableTypes.Contains(containedItem.Type))
            {
                string type = "Cooked" + containedItem.Type;
                containedItem.Type = (FoodType)System.Enum.Parse(typeof(FoodType), type);

                Debug.Log("Finished cooking");
            }

            if(cookTime <= 0)
            {
                state = MicrowaveState.Off;
                Debug.Log("DING!");
                SoundManager.AddSound(6, transform.position, SoundManager.microwaveBeep, audioSource);
            }
        }
    }

    //use for initialization
    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();

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
        //starts an item cooking
        if (player.HeldItem != null && containedItem == null && state == MicrowaveState.Off && cookableTypes.Contains(player.HeldItem.Type))
        {
            containedItem = player.HeldItem;
            player.HeldItem = null;

            state = MicrowaveState.Cooking;

            cookTime = 10;
            cookedInTime = 8;

            SoundManager.AddSound(4, transform.position, SoundManager.microwaveOpen, audioSource);

            Debug.Log("Player left: " + containedItem.Type);
        }
        //player gets item from microwave
        else if (player.HeldItem == null && containedItem != null)
        {
            player.HeldItem = containedItem;
            containedItem = null;

            SoundManager.AddSound(4, transform.position, SoundManager.microwaveOpen, audioSource);

            Debug.Log("Player got: " + player.HeldItem.Type);

            recipie.UpdateList(player.HeldItem.Type);

            state = MicrowaveState.Off;
        }
        else
        {
            //doesn't work
        }
    }
}
