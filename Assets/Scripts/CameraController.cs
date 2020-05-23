using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 offsetToPlayer;
    [SerializeField] Transform playerTransform;

    private void Start() {
        offsetToPlayer = transform.position - playerTransform.position;
    }

    private void LateUpdate() {
        transform.position = playerTransform.position + offsetToPlayer;
    }
}
