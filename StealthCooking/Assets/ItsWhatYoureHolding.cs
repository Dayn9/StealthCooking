using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItsWhatYoureHolding : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] private Image textOfThingPlayerIsHolding;

    //private void Awake()
    //{
    //    textOfThingPlayerIsHolding = this.GetComponent<Text>();
    //}

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (player.HeldItem != null)
        {
            Color seent = textOfThingPlayerIsHolding.color;
            seent.a = 100;
            textOfThingPlayerIsHolding.color = seent;
            textOfThingPlayerIsHolding.sprite = FoodSpriteHolder.foodSprites[player.HeldItem.Type];
        }
        else
        {
            Color zero = textOfThingPlayerIsHolding.color;
            zero.a = 0;
            textOfThingPlayerIsHolding.color = zero;
        }
	}
}
