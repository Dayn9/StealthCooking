using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ripple : MonoBehaviour {

    private float maxSize; //max radius width in Unity units
    private static float growSpeed = 0.1f; //how fast sound travels
    private float alpha;
    private float fadeRate; //how fast sound fades
    private new SpriteRenderer renderer;

    public float MaxSize {
        get { return maxSize; }
        set
        {
            if (value > 0)
            {
                maxSize = value;
                fadeRate = growSpeed / maxSize;
                alpha = 1.0f;
            }
        }
    }

    // Update is called once per frame
    void Update () {

        if (transform.localScale.x <= maxSize)
        {
            transform.localScale += new Vector3(growSpeed, growSpeed, growSpeed);
            alpha -= fadeRate;
            renderer.material.SetFloat("_AlphaChannel", alpha);
            //renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, renderer.color.a - fadeRate);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        alpha = 1.0f;
    }
}
