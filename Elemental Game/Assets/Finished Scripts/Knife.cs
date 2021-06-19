using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public float damage;
    public float range;
    public float impactForce;
    public LineRenderer bulletTrail;
    public Transform shootPoint;

    public Camera fpsCam;
    public GameObject impactEffect;
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            ///if the target doesen t have a rigidbody it will take damage if it does it will be pushed back
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            SpawnBulletTrail(hit.point);

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 0.2f);

        }

        else
        ///if it doesen t hit anything the trail will still play
        {
            SpawnBulletTrail(shootPoint.forward * range);
        }
    }
    private void SpawnBulletTrail(Vector3 hitPoint)
    {
        GameObject bulletTrailEffect = Instantiate(bulletTrail.gameObject, shootPoint.position, Quaternion.identity);

        LineRenderer lineR = bulletTrailEffect.GetComponent<LineRenderer>();

        lineR.SetPosition(0, shootPoint.position);
        lineR.SetPosition(1, hitPoint);

        Destroy (bulletTrailEffect, 0.1f);
    }
}
