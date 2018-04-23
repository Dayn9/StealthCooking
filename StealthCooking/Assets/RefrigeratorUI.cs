using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefrigeratorUI : MonoBehaviour
{
    [SerializeField] private Refrigerator refrigerator;
    [SerializeField] private List<Image> UIImages;
    [SerializeField] private List<Text> texts;

    private void Update()
    {
        if(refrigerator.State == RefrigeratorState.Open)
        {
            for (int i = 0; i < UIImages.Count; i++)
            {
                Color zero = UIImages[i].color;
                zero.a = 100;

                UIImages[i].color = zero;
            }


            texts[0].text = refrigerator.items;
            texts[1].text = "Refrigerator";
        }
        else
        {

            for (int i = 0; i < UIImages.Count; i++)
            {
                Color zero = UIImages[i].color;
                zero.a = 0;

                UIImages[i].color = zero;
            }

            texts[0].text = "";
            texts[1].text = "";
        }
    }
}
