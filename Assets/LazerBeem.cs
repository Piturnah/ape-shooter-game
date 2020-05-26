using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerBeem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Chimp") {
            FindObjectOfType<ChimpController>().DeadChimp(other.gameObject);
        }
    }
}
