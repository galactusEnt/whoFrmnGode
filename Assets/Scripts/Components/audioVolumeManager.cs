using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class audioVolumeManager : MonoBehaviour
{
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat("SoundVolume", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (audioSource.volume == PlayerPrefs.GetFloat("SoundVolume", 1.0f)) return;
        audioSource.volume = PlayerPrefs.GetFloat("SoundVolume", 1.0f);
    }
}
