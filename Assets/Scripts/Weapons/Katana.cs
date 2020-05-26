using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : Weapon
{
    float attackRadius = 1f;

    private void Start() {
        timeBetweenFiring = base.timeBetweenFiring;
    }

    public override void FireWeapon() {
        Debug.Log("done the thing");


    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(base.playerTransform.position, attackRadius);
    }
}
