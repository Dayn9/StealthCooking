using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UI.Selectable;

public class SoundManager : MonoBehaviour {

    private static float soundLevel;
    [SerializeField] private float soundDecayRate; //how fast the noise dies down
    [SerializeField] private float maxSoundLevel;
    [SerializeField] private Slider soundMeter;
    [SerializeField] private GameObject ripplePrefab;

	// Use this for initialization
	void Start () {
        soundLevel = 0.0f;
        soundMeter.minValue = 0;
        soundMeter.maxValue = maxSoundLevel;
        soundMeter.interactable = false;
        soundMeter.value = soundMeter.minValue;

        soundLevel = 5;
	}

    /// <summary>
    /// Add noise to the total Sound
    /// </summary>
    /// <param name="volume">volume of sound to add (max is 10) </param>
    public void AddSound(float volume)
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
    public void AddSound(float volume, Vector2 position)
    {
        AddSound(volume);
        Instantiate(ripplePrefab, position, ripplePrefab.transform.rotation);
    }

    private void WakeParents()
    {

    }

	// Update is called once per frame
	void Update () {
        soundLevel -= soundDecayRate;
        Mathf.Clamp(soundLevel, 0, maxSoundLevel);
        soundMeter.value = soundLevel;
    }
}
