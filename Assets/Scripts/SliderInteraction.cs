using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderInteraction : ComponentsInteractions
{
    Vector3 startingPosition;
    Vector3 targetPosition;

    Vector3 initialHandPosition;

    bool grabbed;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInteracting)
        {
            initialHandPosition = startingPosition;
            grabbed = false;
            MoveSlider(startingPosition, speed);
        }
    }

    public void MoveSlider(Vector3 handlerPosition, float speed)
    {
       // targetPosition = handlerPosition;

        if(!grabbed && isInteracting)
        {
            initialHandPosition = handlerPosition;
            grabbed = true;
        }

        float distance = -Vector3.Distance(initialHandPosition, handlerPosition);

        if (isInteracting)
        {
            targetPosition = new Vector3(
                            transform.localPosition.x,
                            transform.localPosition.y,
                            distance
                            );
        }
        else
        {
            targetPosition = startingPosition;
        }
        
        //Debug.Log(targetPosition.z);

        targetPosition.z = Mathf.Clamp(targetPosition.z, -0.04f, 0.012f);
        

        transform.localPosition = ComponentMovement.ComponentPosition(
                    transform.localPosition,
                    targetPosition,
                    speed);

        //Debug.Log(transform.localPosition.z);
    }
}
