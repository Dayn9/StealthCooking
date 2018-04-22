using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    // Fields
    [SerializeField] private float lerpAmount;
    [SerializeField] private GameObject subject;

    // Methods
    public void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(subject.transform.position.x, transform.position.y, subject.transform.position.z), lerpAmount);
    }
}
