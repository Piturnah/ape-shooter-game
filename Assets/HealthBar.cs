using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Image image;
    PlayerController player;

    private void Start() {
        image = GetComponent<Image>();
        player = FindObjectOfType<PlayerController>();
    }

    private void Update() {
        image.fillAmount = (float)player.health / player.maxHealth;
    }
}
