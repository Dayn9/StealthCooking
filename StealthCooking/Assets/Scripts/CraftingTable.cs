using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : Appliance
{
    [SerializeField] private Food storedItem;

    public override void Interact(Player player)
    {
        if(player.HeldItem == null)
        {
            player.HeldItem = storedItem;
            storedItem = null;
        }
        else if(storedItem == null)
        {
            storedItem = player.HeldItem;
            player.HeldItem = null;
        }
        else
        {
            //test for combination
        }
    }
}
