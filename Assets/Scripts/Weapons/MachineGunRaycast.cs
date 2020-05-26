using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunRaycast : MonoBehaviour
{
    public int gunDamage = 1;
    public float fireRate = 0f;
    public float weaponRange = 50f;
    public float hitForce = 100f;

    public Transform gunEnd;

    
    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    //private AudioSource gunAudio;
    private LineRenderer laserLine;
    private float nextFire;

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        //gunAudio = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();
    }

    void Update()
    {
        if (Input.GetButton ("Fire1") && Time.time > nextFire)
        {
            StartCoroutine(ShotEffect());
            nextFire = Time.time + fireRate;
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));
            laserLine.SetPosition(0, gunEnd.position);
            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out RaycastHit hit, weaponRange))
            {
                laserLine.SetPosition(1, hit.point);
            }
            else
            {
                laserLine.SetPosition(1, fpsCam.transform.position + fpsCam.transform.forward * weaponRange);
            }
        }
    }

    private IEnumerator ShotEffect()
    {
       //gunAudio.Play();
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;

    }
}
