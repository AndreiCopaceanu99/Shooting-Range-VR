using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderInteraction : ComponentsInteractions
{
    Vector3 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInteracting)
        {
            /*transform.localPosition = ComponentMovement.ComponentPosition(
                    transform.localPosition,
                    startingPos,
                    200f);*/
            MoveSlider(startingPos, 200f);
        }
    }

    public void MoveSlider(Vector3 handlerPosition, float speed)
    {
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
