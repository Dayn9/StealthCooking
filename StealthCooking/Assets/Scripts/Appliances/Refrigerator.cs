using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RefrigeratorState { Open, Closed }

public class Refrigerator : Appliance
{
    [SerializeField] private List<FoodType> initialItems;
    private List<Food> storedItems;

    private Player player;

    private RefrigeratorState state;

    //use for initialization
    private void Awake()
    {
        storedItems = new List<Food>();
        for(int i = 0; i < initialItems.Count; i++)
        {
            storedItems.Add(gameObject.AddComponent<Food>());
            storedItems[i].Type = initialItems[i];
        }

        state = RefrigeratorState.Closed;
    }

    //runs once per frame
    private void Update()
    {
        if(state == RefrigeratorState.Open)
        {
            //checks to see if the user is pressing one of the number keys
            int selectedNum = -1;
            if (Input.GetKeyDown(KeyCode.Alpha1)) { selectedNum = 0; }
            else if (Input.GetKeyDown(KeyCode.Alpha2)) { selectedNum = 1; }
            else if (Input.GetKeyDown(KeyCode.Alpha3)) { selectedNum = 2; }
            else if (Input.GetKeyDown(KeyCode.Alpha4)) { selectedNum = 3; }
            else if (Input.GetKeyDown(KeyCode.Alpha5)) { selectedNum = 4; }
            else if (Input.GetKeyDown(KeyCode.Alpha6)) { selectedNum = 5; }
            else if (Input.GetKeyDown(KeyCode.Alpha7)) { selectedNum = 6; }
            else if (Input.GetKeyDown(KeyCode.Alpha8)) { selectedNum = 7; }
            else if (Input.GetKeyDown(KeyCode.Alpha9)) { selectedNum = 8; }

            //if the user pressed a number that corresponds to an item in the refrigerator,
            //the player takes it from the refrigerator
            if (selectedNum < storedItems.Count && selectedNum != -1)
            {
                player.HeldItem = storedItems[selectedNum];
                storedItems.Remove(player.HeldItem);
                player.State = PlayerState.Waiting;

                Debug.Log("Player got: " + player.HeldItem.Type);
            }
        }
    }

    /// <summary>
    /// Allows player to interact with refrigerator
    /// </summary>
    public override void Interact(Player player)
    {
        this.player = player;

        //player puts item into the refrigerator
        if(player.HeldItem != null && storedItems.Count < 9)
        {
            player.State = PlayerState.Interacting;

            storedItems.Add(player.HeldItem);
            player.HeldItem = null;

            Debug.Log("Player left : " + storedItems[storedItems.Count - 1].Type);
        }
        else if((player.HeldItem != null && storedItems.Count >= 9) || storedItems.Count == 0)
        {
            //doesn't work
            return;
        }

        //sets player to be interacting with the refrigerator
        if (player.HeldItem == null)
        {
            player.State = PlayerState.Interacting;

            string items = "";
            for (int i = 0; i < storedItems.Count; i++)
            {
                items += i + 1 + ": " + storedItems[i].Type + "   ";
            }

            state = RefrigeratorState.Open;

            Debug.Log(items);
        }
    }
}
