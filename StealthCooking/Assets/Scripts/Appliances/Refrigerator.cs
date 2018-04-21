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

    private List<KeyCode> numberKeys;


    private void Awake()
    {
        storedItems = new List<Food>();
        for(int i = 0; i < initialItems.Count; i++)
        {
            storedItems.Add(gameObject.AddComponent<Food>());
            storedItems[i].Type = initialItems[i];
        }

        state = RefrigeratorState.Closed;

        numberKeys = new List<KeyCode>
        {
            KeyCode.Alpha1,
            KeyCode.Alpha2,
            KeyCode.Alpha3,
            KeyCode.Alpha4,
            KeyCode.Alpha5,
            KeyCode.Alpha6,
            KeyCode.Alpha7,
            KeyCode.Alpha8,
            KeyCode.Alpha9
        };

    }

    private void Update()
    {
        if(state == RefrigeratorState.Open)
        {
            //checks to see if the user is pressing one of the number keys
            int selectedNum = -1;
            for(int i = 0; i < numberKeys.Count; i++)
            {
                if (Input.GetKeyDown(numberKeys[i]))
                {
                    selectedNum = i;
                    i = numberKeys.Count;
                }
            }

            //if the user pressed a number that corresponds to an item in the refrigerator,
            //the player takes it from the refrigerator
            if (storedItems.Count <= selectedNum + 1 && selectedNum != -1)
            {
                player.HeldItem = storedItems[selectedNum];
                storedItems.Remove(player.HeldItem);
                player.State = PlayerState.Waiting;

                Debug.Log("Player got: " + player.HeldItem.Type);
            }
        }
    }

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
            string items = "";
            for (int i = 0; i < storedItems.Count; i++)
            {
                items += i + ": " + storedItems[i];
            }

            state = RefrigeratorState.Open;

            Debug.Log(items);
        }
    }
}
