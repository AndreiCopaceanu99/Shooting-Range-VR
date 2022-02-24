using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimation : MonoBehaviour
{
    Animator animator;

    public bool shoot;

    [SerializeField] GameObject shootingModel;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("shooting", shoot);

        if(shoot)
        {
            
        }
    }
}
