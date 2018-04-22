using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSprite : MonoBehaviour
{
    // Fields
    [SerializeField] private Transform position;

    // Methods
    public void LateUpdate()
    {
        transform.position = position.position;
    }
}
