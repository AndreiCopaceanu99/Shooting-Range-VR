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
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            if(safetyOn)
            {
                RotateSafetyLock(initialRotation, lockedRotation);
            }
            else
            {
                RotateSafetyLock(lockedRotation, initialRotation);
            }
        }

        //Debug.Log(transform.localEulerAngles);
    }

    public void ChangeSafetyPosition()
    {
        /*if (!safetyOn && transform.localEulerAngles.x == initialRotation.x)
        {
            move = true;
            safetyOn = !safetyOn;
        }
        else if(safetyOn && transform.localEulerAngles.x == lockedRotation.x)
        {
            move = true;
            safetyOn = !safetyOn;
        }*/

        if(!move)
        {
            move = true;
            safetyOn = !safetyOn;
        }
    }

    void RotateSafetyLock(Vector3 startingRotation, Vector3 targetRotation)
    {
        //transform.localRotation = Quaternion.Euler(ComponentMovement.ComponentPosition(startingRotation, targetRotation, 200f));
        transform.localEulerAngles = ComponentMovement.ComponentPosition(startingRotation, targetRotation, speed);

        //Debug.Log(transform.localEulerAngles + " " + targetRotation);
        if (transform.localEulerAngles == targetRotation)
        {
            move = false;
        }
    }
}
