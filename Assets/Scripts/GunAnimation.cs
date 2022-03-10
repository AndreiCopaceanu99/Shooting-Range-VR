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
        animator.enabled = gunManager.shoot;
        animator.SetBool("shooting", gunManager.shoot);

        if(gunManager.shoot)
        {
            //animator.enabled = true;
            Sounds sounds = GetComponent<Sounds>();
            sounds.PlaySound();
        }
        /*else
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Shooting"))
            {
                animator.enabled = false;
            }
        }*/
    }
}
