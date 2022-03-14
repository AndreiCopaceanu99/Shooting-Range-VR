using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafetyLock : ComponentsInteractions
{
    bool safetyOn = false;
    bool move = false;

    Vector3 initialRotation;
    [SerializeField] Vector3 lockedRotation;

    // Start is called before the first frame update
    void Start()
    {
        initialRotation = transform.localEulerAngles;
        gunManager = FindObjectOfType<GunManager>();
    }

    // Update is called once per frame
    void Update()
    {
        gunManager.safetyOn = safetyOn;
        if (move)
        {
            if(safetyOn)
            {
                RotateSafetyLock(lockedRotation);
            }
            else
            {
                RotateSafetyLock(initialRotation);
            }
        }
    }

    public void ChangeSafetyPosition()
    {
        if(!move)
        {
            move = true;
            safetyOn = !safetyOn;
        }
    }

    void RotateSafetyLock(Vector3 targetRotation)
    {
        transform.localEulerAngles = ComponentMovement.ComponentPosition(transform.localEulerAngles, targetRotation, speed);

        if (transform.localEulerAngles == targetRotation)
        {
            move = false;
        }
    }
}
