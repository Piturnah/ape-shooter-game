using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity;

    private void Awake() {
        LockCursor();
    }

    void LockCursor() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        if (!FindObjectOfType<PlayerController>().dead) {
            CameraRotation();
        }
        
    }

    void CameraRotation() {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(-Vector3.right * mouseY, Space.Self);
        transform.parent.Rotate(Vector3.up * mouseX, Space.Self);
    }
}
