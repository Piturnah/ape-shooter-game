﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Weapon : MonoBehaviour
{
    public Animator anim;
    public float timeBetweenFiring;
    public string firingAnimName;

    float lastShotTime;

    private void Start() {
        anim = GetComponent<Animator>();
    }

    private void Update() {
        if (Input.GetKey(KeyCode.Mouse0)) {
            if (Time.time > lastShotTime + timeBetweenFiring) {
                lastShotTime = Time.time;
                FireWeapon();
            }
        }
    }

    void FireWeapon() {
        anim.Play(firingAnimName);
    }
}
