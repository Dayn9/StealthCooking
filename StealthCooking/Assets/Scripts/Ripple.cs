using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ripple : MonoBehaviour {

    private float maxSize=10;
    private float growSpeed = 0.5f;

    /// <summary>
    /// start the Ripple Effect
    /// </summary>
    /// <param name="maxSize">based on volume of sound</param>
    public void Begin(float maxSize)
    {
        this.maxSize = maxSize;
    }

    // Update is called once per frame
    void Update () {
        if (transform.localScale.x < maxSize)
        {
            transform.localScale += new Vector3(growSpeed, 0, growSpeed);
        }
    }  
}
