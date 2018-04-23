using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDisplayCrafting : MonoBehaviour
{

    [SerializeField] private SpriteRenderer foodSprite;

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.parent.GetComponent<CraftingTable>().StoredItem != null)
        {
            foodSprite.sprite = FoodSpriteHolder.foodSprites[gameObject.transform.parent.GetComponent<CraftingTable>().StoredItem.Type];
        }
        else
        {
            foodSprite.sprite = null;
        }
    }
}
