using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ripple : MonoBehaviour {

    private float maxSize = 10; //max radius width in Unity units
    private static float growSpeed = 0.1f; //how fast sound travels
    private float fadeRate = 0; //how fast sound fades
    private new SpriteRenderer renderer;

    public float MaxSize {
        set
        {
            if (value > maxSize)
            {
                maxSize = value;
                fadeRate = growSpeed / maxSize;
            }
        }
    }

    // Update is called once per frame
    void Update () {

        if (transform.localScale.x <= maxSize)
        {
            transform.localScale += new Vector3(growSpeed, growSpeed, growSpeed);
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, renderer.color.a - fadeRate);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        fadeRate = growSpeed / maxSize;
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }
}
