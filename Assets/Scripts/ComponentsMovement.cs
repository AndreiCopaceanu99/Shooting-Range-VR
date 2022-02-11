using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ComponentMovement
{
    public static Vector3 ComponentPosition(this Vector3 startingPosition, Vector3 targetPosition, float speed)
    {
        Vector3 position = Vector3.Lerp(
            startingPosition, 
            new Vector3(startingPosition.x, startingPosition.y, Mathf.Clamp(-targetPosition.z, 0.012f, -0.04f)), 
            Vector3.Distance(startingPosition, targetPosition) * Time.deltaTime * speed
            );
        return position;
    }
}
