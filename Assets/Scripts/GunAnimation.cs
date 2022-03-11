using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimation : MonoBehaviour
{
    Animator animator;

    GunManager gunManager;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //animator.enabled = false;
        gunManager = GetComponent<GunManager>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.enabled = gunManager.shoot && gunManager.canShoot && gunManager.hasBullets;
        animator.SetBool("shooting", gunManager.shoot && gunManager.canShoot && gunManager.hasBullets);

        if (animator.enabled)
        {
            //animator.enabled = true;
            Sounds sounds = GetComponent<Sounds>();
            sounds.PlaySound();
        }
    }
}
