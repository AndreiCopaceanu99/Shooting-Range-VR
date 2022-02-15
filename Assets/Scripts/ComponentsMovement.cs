using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ComponentMovement
{
    public static Vector3 ComponentPosition(this Vector3 startingPosition, Vector3 targetPosition, float speed)
    {
        /*Vector3 position = Vector3.Lerp(
            startingPosition, 
            targetPosition, 
            Vector3.Distance(startingPosition, targetPosition) * Time.deltaTime * speed
            );*/
        Vector3 position = Vector3.Lerp(
            startingPosition,
            targetPosition,
            speed * Time.deltaTime
            );
        return position;
    }
}
