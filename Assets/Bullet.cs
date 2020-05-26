using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 100;
    public Vector3 direction;

    private void Update() {
        transform.Translate(direction * speed * Time.deltaTime);

        if (transform.position.magnitude >= 200) {
            Destroy(gameObject);
        }
    }
}
