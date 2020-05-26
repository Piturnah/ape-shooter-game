using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Railgun : MonoBehaviour
{
    float lazerCooldownTime = 5;
    float lazerActiveTime = 5;
    float lazerChargeTime = 2;

    bool charging;
    float startedChargingTime;

    public GameObject lazerBeam;

    private void Update() {
        lazerBeam.SetActive(Input.GetKey(KeyCode.Mouse0) && !FindObjectOfType<PlayerController>().dead);
    }
}
