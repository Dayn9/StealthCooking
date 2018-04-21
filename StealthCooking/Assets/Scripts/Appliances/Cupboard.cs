using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cupboard : Appliance
{
    [SerializeField] private Food containedItem;

    /// <summary>
    /// Allows the player to interact with this appliance
    /// </summary>
    public override void Interact(Player player)
    {
        if(player.HeldItem == null && containedItem != null)
        {
            player.HeldItem = containedItem;
            containedItem = null;
        }
        else if(containedItem == null && player.HeldItem != null)
        {
            containedItem = player.HeldItem;
            player.HeldItem = null;
        }
        else
        {
            //Doesn't work
        }
    }
}
