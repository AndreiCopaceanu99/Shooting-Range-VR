using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public bool shoot;

    [SerializeField] GameObject muzzleFlash;
    // Start is called before the first frame update
    void Start()
    {
        muzzleFlash.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(shoot)
        {
            muzzleFlash.SetActive(true);
        }
        else
        {
            muzzleFlash.SetActive(false);
        }
    }
}
