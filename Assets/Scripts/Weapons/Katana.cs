using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : Weapon
{
    private void Start() {
        timeBetweenFiring = base.timeBetweenFiring;
    }

    public override void FireWeapon() {
        Debug.Log("done the thing");
    }
}
