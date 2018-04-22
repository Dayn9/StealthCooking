using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Appliance : MonoBehaviour
{
    /// <summary>
    /// Allows for playr interaction with appliances
    /// </summary>
    public abstract void Interact(Player player);
}
