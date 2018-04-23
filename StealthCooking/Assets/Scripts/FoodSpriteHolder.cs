using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodSpriteHolder : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private List<FoodType> food;

    public static Dictionary<FoodType, Sprite> foodSprites;

    private void Start()
    {
        foodSprites = new Dictionary<FoodType, Sprite>();

        for(int i = 0; i < sprites.Count; i++)
        {
            foodSprites[food[i]] = sprites[i];
        }
    }
}
