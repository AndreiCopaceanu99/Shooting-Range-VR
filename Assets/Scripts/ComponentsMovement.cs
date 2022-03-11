using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ComponentMovement
{
    public static Vector3 ComponentPosition(this Vector3 startingPosition, Vector3 targetPosition, float speed)
    {
        Vector3 position = Vector3.Lerp(
            startingPosition,
            targetPosition,
            speed * Time.deltaTime
            );
        //Debug.Log(targetPosition);

        //Debug.Log(startingPosition.z + " " + targetPosition.z + " " + position.z + " " + speed);
        return position;
    }
}