using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodDisplayCupboard : MonoBehaviour
{
    [SerializeField] private SpriteRenderer foodSprite;

    // Update is called once per frame
    void Update ()
    {
        if (gameObject.transform.parent.GetComponent<Cupboard>().StoredItem != null)
        {
            foodSprite.sprite = FoodSpriteHolder.foodSprites[gameObject.transform.parent.GetComponent<Cupboard>().StoredItem.Type];
        }
        else
        {
            foodSprite.sprite = null;
        }
    }
}
