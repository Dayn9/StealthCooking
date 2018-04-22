using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//holds audio files
public class SoundHolder : MonoBehaviour {

    [SerializeField] public AudioClip[] footsteps;
    [SerializeField] public AudioClip[] microwave;
    [SerializeField] public AudioClip[] fridge;
    [SerializeField] public AudioClip pickup;
    [SerializeField] public AudioClip place;
    [SerializeField] public AudioClip bark;
    [SerializeField] public AudioClip squish;
    [SerializeField] public AudioClip chop;

    [SerializeField] public GameObject legalGuardian;
}
