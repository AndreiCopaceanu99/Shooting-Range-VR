using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public bool shoot;

    [SerializeField] Transform bulletSpawn;
    [SerializeField] Transform caseSpawn;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject casePrefab;

    [SerializeField] GameObject muzzleFlash;

    [SerializeField] Vector3 bulletForce;
    [SerializeField] Vector3 caseForce;

    bool canShoot;
    // Start is called before the first frame update
    void Start()
    {
        muzzleFlash.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        MuzzleFlash();
    }

    private void FixedUpdate()
    {
        Shoot();
    }

    void MuzzleFlash()
    {
        if (shoot)
        {
            muzzleFlash.SetActive(true);
        }

        else
        {
            muzzleFlash.SetActive(false);
        }
    }

    void Shoot()
    {
        if (shoot)
        {
            if (canShoot)
            {
                Rigidbody bulletRB = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity).GetComponent<Rigidbody>();
                Rigidbody caseRB = Instantiate(casePrefab, caseSpawn.position, Quaternion.identity).GetComponent<Rigidbody>();

                bulletRB.AddRelativeForce(transform.forward * bulletForce.z);
                caseRB.AddRelativeForce(transform.right * caseForce.x + transform.up * caseForce.y);

                canShoot = false;
            }
        }
        else
        {
            canShoot = true;
        }
    }
}
