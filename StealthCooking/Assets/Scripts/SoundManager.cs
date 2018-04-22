using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//using UnityEngine.UI.Selectable;

public class SoundManager : MonoBehaviour {

    private static float soundLevel;
    [SerializeField] private float soundDecayRate; //how fast the noise dies down
    private static float maxSoundLevel = 20;
    //[SerializeField] private Slider soundMeter;
    [SerializeField] private GameObject ripplePrefab;
    private static GameObject ripple;

    private static AudioSource audioSource; //Sound managers Audio Source
    [SerializeField] public AudioClip[] footsteps;
    [SerializeField] public AudioClip MicrowaveBeep;
    [SerializeField] public AudioClip Hum;

    System.Random rand;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();

        soundLevel = 0.0f;
        //soundMeter.minValue = 0;
        //soundMeter.maxValue = maxSoundLevel;
        //soundMeter.interactable = false;
        //soundMeter.value = soundMeter.minValue;

        ripple = ripplePrefab;

        rand = new System.Random();
    }

    /// <summary>
    /// Add noise to the total Sound
    /// </summary>
    /// <param name="volume">volume of sound to add (max is 10) </param>
    public static void AddSound(float volume)
    {
        if (volume > 0)
        {
            soundLevel += volume;
            //sound level above max threshold
            if (soundLevel > maxSoundLevel) {
                WakeParents();
                soundLevel = maxSoundLevel;
            }
        }

    }
    /// <summary>
    /// Add noise to the total Sound at a specific location
    /// </summary>
    /// <param name="volume">volume of sound to add (max is 10) </param>
    /// <param name="position">location of the sound</param>
    public static void AddSound(float volume, Vector3 position)
    {
        AddSound(volume);
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

    private static void WakeParents()
    {

    }

	// Update is called once per frame
	void Update () {
        soundLevel -= soundDecayRate;
        Mathf.Clamp(soundLevel, 0, maxSoundLevel);
        //soundMeter.value = soundLevel;

        if (Input.GetKeyDown(KeyCode.P))
        {
            AddSound(2f, new Vector3(0, 10, 0), MicrowaveBeep, audioSource);
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
