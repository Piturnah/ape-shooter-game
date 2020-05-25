using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask floorLayers;
    [SerializeField] float movementSpeed;

    private void Update() {
        //ReceiveRotationInput();
        ReceiveMovementInput();
    }

    void ReceiveMovementInput() {
        Vector3 inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        transform.Translate(inputDir * Time.deltaTime * movementSpeed, Space.Self);
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
