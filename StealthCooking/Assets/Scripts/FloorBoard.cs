using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBoard : MonoBehaviour
{
    [SerializeField] private Collider target;
    private float volume = 3f;
    private AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other == target)
        {
            SoundManager.AddSound(volume, transform.position, SoundManager.bark, audioSource);
        }
    }
}
