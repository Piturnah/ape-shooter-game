using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask floorLayers;
    [SerializeField] float movementSpeed;

    public int maxHealth;
    public int health;
    public int damageFromChimp;
    float attackCooldownTime = 0.5f;
    float lastAttackTime;

    public bool dead;

    public GameObject deadScreen;

    private void Start() {
        health = maxHealth;
    }

    private void Update() {
        if (health <= 0) {
            dead = true;
            deadScreen.SetActive(true);
        }

        if (!dead) {
            ReceiveMovementInput();
        }
    }

    void ReceiveMovementInput() {
        Vector3 inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        transform.Translate(inputDir * Time.deltaTime * movementSpeed, Space.Self);
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Chimp" && Time.time > lastAttackTime + attackCooldownTime) {
            Debug.Log("Yes");
            lastAttackTime = Time.time;
            health -= damageFromChimp;
        }
    }

    void ReceiveRotationInput() {

        Vector3 mouseScreenPosition = Input.mousePosition;
        RaycastHit hit;
        Vector3 mouseWorldPos = new Vector3();
        Ray mouseRay = Camera.main.ScreenPointToRay(mouseScreenPosition);

        if (Physics.Raycast(mouseRay, out hit, int.MaxValue, floorLayers)) {
            mouseWorldPos = hit.point;
        }
        
        Vector2 xzDisplacementFromMousePos = new Vector2(mouseWorldPos.x, mouseWorldPos.z) - new Vector2(transform.position.x, transform.position.z);
        float radiansToMousePos = Mathf.Atan2(xzDisplacementFromMousePos.y, xzDisplacementFromMousePos.x);
        transform.eulerAngles = Vector3.up * (90 - radiansToMousePos * Mathf.Rad2Deg);
    }
}
