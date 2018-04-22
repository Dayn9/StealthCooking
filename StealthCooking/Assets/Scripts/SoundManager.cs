using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SoundHolder))]
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour {

    private static float soundLevel;
    [SerializeField] private float soundDecayRate; //how fast the noise dies down
    private static float maxSoundLevel = 20;
    [SerializeField] private Slider soundMeter;
    [SerializeField] private GameObject ripplePrefab;
    private static GameObject ripple;

    private static AudioSource audioSource; //Sound managers Audio Source

    public static AudioClip[] footsteps;
    public static AudioClip microwaveOpen, microwaveHum, microwaveClose, microwaveBeep, 
                            fridgeOpen, fridgeHum, fridgeClose, 
                            pickup, place;

    System.Random rand;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        
        //Read in the sound files from the sound holder
        SoundHolder holder = GetComponent<SoundHolder>();
        footsteps = holder.footsteps;
        microwaveOpen = holder.microwave[0];
        microwaveHum = holder.microwave[1];
        microwaveClose = holder.microwave[2];
        microwaveBeep = holder.microwave[3];
        fridgeOpen = holder.fridge[0];
        fridgeHum = holder.fridge[0];
        fridgeClose = holder.fridge[0];
        fridgeOpen = holder.pickup;
        fridgeOpen = holder.place;

        soundLevel = 0.0f;
        soundMeter.minValue = 0;
        soundMeter.maxValue = maxSoundLevel;
        soundMeter.interactable = false;
        soundMeter.value = soundMeter.minValue;

        ripple = ripplePrefab;

        rand = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        soundLevel -= soundDecayRate;
        if (soundLevel < 0) { soundLevel = 0; }
        if (soundLevel > maxSoundLevel) { soundLevel = maxSoundLevel; }
        soundMeter.value = soundLevel;

        if (Input.GetKeyDown(KeyCode.P))
        {
            AddSound(2f, new Vector3(0, 10, 0), microwaveBeep, audioSource);
        }
    }

    /// <summary>
    /// Add noise to the total Sound at a specific location
    /// </summary>
    /// <param name="volume">volume of sound to add (max is 10) </param>
    /// <param name="position">location of the sound</param>
    public static void AddSound(float volume, Vector3 position)
    {
        //Play the Sound
        if (volume > 0)
        {
            soundLevel += volume;
            //sound level above max threshold
            if (soundLevel > maxSoundLevel)
            {
                WakeParents();
                soundLevel = maxSoundLevel;
            }
        }
        //Create the Ripple
        Instantiate(ripple, position, ripple.transform.rotation).GetComponent<Ripple>().MaxSize = volume ;
    }
    /// <summary>
    /// Add noise to the total Sound at a specific location and plays a sound
    /// </summary>
    /// <param name="volume">volume of sound to add (max is 10) </param>
    /// <param name="playerPosition">ocation of the sound</param>
    /// <param name="clip">Audio Clip to Play</param>
    /// <param name="source">AudioSource component of object playing sound</param>
    public static void AddSound(float volume, Vector3 position, AudioClip clip, AudioSource source)
    {
        AddSound(volume,position);
        PlaySound(clip, source);
    }

    /// <summary>
    /// Add noise to the total Sound at a specific location and plays a sound if not already
    /// </summary>
    /// <param name="volume">volume of sound to add (max is 10) </param>
    /// <param name="playerPosition">ocation of the sound</param>
    /// <param name="clip">Audio Clip to Play</param>
    /// <param name="source">AudioSource component of object playing sound</param>
    public static void AddSoundContinuous(float volume, Vector3 position, AudioClip clip, AudioSource source)
    {
        AddSound(volume, position);
        PlaySoundContinuous(clip, source);
    }

    //-----------------------------------------------------------------------------<<< ADD CODE FOR SPAWNING LEGAL GUARDIAN HERE <<<
    private static void WakeParents()
    {

    }


    /// <summary>
    /// Only plays sound if source isn't already
    /// </summary>
    /// <param name="clip">Audio Clip to Play</param>
    /// <param name="source">AudioSource component of object playing sound<</param>
    private static void PlaySoundContinuous(AudioClip clip, AudioSource source)
    {
        if (!audioSource.isPlaying)
        {
            source.clip = clip;
            source.Play();
        }
    }

    /// <summary>
    /// Play footstep noises if not already playing
    /// </summary>
    private void PlayFootStepSound()
    {
        if (!audioSource.isPlaying)
        {
            PlaySound(footsteps[rand.Next(footsteps.Length)]);
        }
    }

    /// <summary>
    /// Play a sound from Sound Manager
    /// </summary>
    /// <param name="clip">Audio Clip to Play</param>
    private void PlaySound(AudioClip clip)
    {
        audioSource.Stop(); //override current sound
        audioSource.clip = clip;
        audioSource.Play();
    }

    /// <summary>
    /// Play a sound from a specific object
    /// </summary>
    /// <param name="clip">Audio Clip to Play</param>
    /// <param name="source">AudioSource component of object playing sound</param>
    private static void PlaySound(AudioClip clip, AudioSource source)
    {
        source.Stop();
        source.clip = clip;
        source.Play();
    }
}
