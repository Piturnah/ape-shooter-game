using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Crate : MonoBehaviour
{
    public static event Action cratePickedUp;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            cratePickedUp?.Invoke();
            Destroy(gameObject);
        }
    }
}
