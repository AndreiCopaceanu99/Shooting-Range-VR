using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderInteraction : ComponentsInteractions
{
    Vector3 startingPosition;
    Vector3 targetPosition;

    Vector3 initialHandPosition;

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
            MoveSlider(startingPosition, speed);
            initialHandPosition = targetPosition;
        }
    }

    public void MoveSlider(Vector3 handlerPosition, float speed)
    {
        targetPosition = handlerPosition;
        transform.localPosition = ComponentMovement.ComponentPosition(
                    transform.localPosition,
                    new Vector3(
                        transform.localPosition.x,
                        transform.localPosition.y,
                        Mathf.Clamp(-handlerPosition.z, 0.012f, -0.04f)
                        ),
                    speed);
    }
}
