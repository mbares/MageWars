using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    public static MusicPlayer instance = null;

    public AudioClip[] clips;

    private AudioSource audioSource;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void SetVolume(float volume) {
        audioSource.volume = volume;
    }

    public void SetClip(int clipIndex) {
        audioSource.clip = clips[clipIndex];
        audioSource.Play();
    }
}
