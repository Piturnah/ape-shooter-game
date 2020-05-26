using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimpController : MonoBehaviour
{
    public GameObject deadChimp;

    private void Start() {
        Weapon.killedChimp += DeadChimp;
    }

    public void DeadChimp(GameObject chimp) {
        GameObject deadChimpObj = Instantiate(deadChimp, chimp.transform.position, chimp.transform.rotation);
        deadChimpObj.GetComponent<Rigidbody>().AddExplosionForce(10, deadChimpObj.transform.position, 3);
        Destroy(chimp);
    }
}
