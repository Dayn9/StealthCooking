using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAppliance : Appliance
{
    public override void Interact(Player player)
    {
        Debug.Log("<color=green>Test Success!</color>");
    }
}
