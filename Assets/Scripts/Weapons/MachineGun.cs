using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    public GameObject nozzel;
    public GameObject bullet;
    public float timeBtwShots = 0.1f;
    float prevShotTime;

    private void Update() {
        if (Time.time > prevShotTime + 0.05f) {
            nozzel.GetComponent<MeshRenderer>().enabled = false;
        }
        
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > prevShotTime + timeBtwShots && !FindObjectOfType<PlayerController>().dead) {
            prevShotTime = Time.time;
            nozzel.GetComponent<MeshRenderer>().enabled = true;
            Fire();
        }
    }

    void Fire() {
        RaycastHit hit;
        if (Physics.Raycast(nozzel.transform.position, nozzel.transform.forward, out hit)) {
            if (hit.transform.tag == "Chimp") {
                FindObjectOfType<ChimpController>().DeadChimp(hit.transform.gameObject);
            }
        }
        Bullet newBullet = Instantiate(bullet, nozzel.transform.position, nozzel.transform.rotation).GetComponent<Bullet>();
        newBullet.direction = nozzel.transform.forward;
    }
}
