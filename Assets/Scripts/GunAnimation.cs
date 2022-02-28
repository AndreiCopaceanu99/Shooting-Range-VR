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
        gunManager = GetComponent<GunManager>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("shooting", gunManager.shoot);

        if(gunManager.shoot)
        {
            Sounds sounds = GetComponent<Sounds>();
            sounds.PlaySound();
        }
    }
}
