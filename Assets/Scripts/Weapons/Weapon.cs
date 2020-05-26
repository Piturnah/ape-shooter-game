using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using System;

public class Weapon : MonoBehaviour
{
    Animator anim;
    public float timeBetweenFiring;
    public string firingAnimName;

    public Transform playerTransform;
    public LayerMask attackMask;

    float lastShotTime;

    public static event Action<GameObject> killedChimp;

    private void Start() {
        anim = GetComponent<Animator>();
        playerTransform = FindObjectOfType<PlayerController>().transform;
    }

    public void KilledChimp(GameObject chimpObject) {
        killedChimp?.Invoke(chimpObject);
    }

    private void Update() {
        if (Input.GetKey(KeyCode.Mouse0)) {
            if (Time.time > lastShotTime + timeBetweenFiring) {
                lastShotTime = Time.time;
                FireWeapon();
            }
        }
    }

    public virtual void FireWeapon() {
        anim.Play(firingAnimName);

        Collider[] collidersCollided = Physics.OverlapSphere(playerTransform.position, 5, attackMask);
        Debug.Log(collidersCollided.Length);
        if (collidersCollided != null) {
            foreach (Collider c in collidersCollided) {
                KilledChimp(c.gameObject);
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawSphere(playerTransform.position, 1);
    }
}
