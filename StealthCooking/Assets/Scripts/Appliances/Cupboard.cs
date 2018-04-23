using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cupboard : Appliance
{

    [SerializeField] private AudioSource source;
    [SerializeField] private FoodType startingType;
    private Food storedItem;

    public Food StoredItem { get { return storedItem; } }

    /// <summary>
    /// Sets up starting item
    /// </summary>
    private void Start()
    {
        storedItem = gameObject.AddComponent<Food>();

        if (startingType != FoodType.Null)
        {
            storedItem.Type = startingType;
        }
        else
        {
            storedItem = null;
        }
    }

    /// <summary>
    /// Allows the player to interact with this appliance
    /// </summary>
    public override void Interact(Player player)
    {
        //player gets item from cupboard
        if(player.HeldItem == null && storedItem != null)
        {
            player.HeldItem = storedItem;
            storedItem = null;

            SoundManager.AddSound(2, transform.position, SoundManager.pickup, source);

            Debug.Log("Player got: " + player.HeldItem.Type);
        }
        //player puts item in cupboard
        else if(storedItem == null && player.HeldItem != null)
        {
            storedItem = player.HeldItem;
            player.HeldItem = null;

            SoundManager.AddSound(2, transform.position, SoundManager.place, source);

            Debug.Log("Player left: " + storedItem.Type);
        }
        else
        {
            //Doesn't work
        }
    }
}
