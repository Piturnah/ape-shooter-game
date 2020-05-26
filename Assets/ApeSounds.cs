using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ApeSounds : MonoBehaviour
{
    public Sound[] apeSounds;
    public float soundDelayMin;
    public float soundDDelayMax;

    float timeUntilNextSound;
    float previousSoundTime;

    private void Awake() {
        foreach (Sound s in apeSounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = 1;
            s.source.spatialBlend = 1;
        }
    }

    private void Start() {
        Play(apeSounds[UnityEngine.Random.Range(0, apeSounds.Length)].name);
        timeUntilNextSound = UnityEngine.Random.Range(soundDelayMin, soundDDelayMax);
        previousSoundTime = Time.time;
    }

    private void Update() {
        if (Time.time > previousSoundTime + timeUntilNextSound) {
            Play(apeSounds[UnityEngine.Random.Range(0, apeSounds.Length)].name);
            timeUntilNextSound = UnityEngine.Random.Range(soundDelayMin, soundDDelayMax);
            previousSoundTime = Time.time;
        }
    }

    public void Play(string name) {
        Array.Find(apeSounds, apeSound => apeSound.name == name).source.Play();
    }
}
