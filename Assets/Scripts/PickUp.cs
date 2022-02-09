using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PickUp
{
    public static Vector3 PickUpObject(this Vector3 startingPosition, Vector3 targetPosition)
    {
        float lerpPosition = 0;
        lerpPosition += Time.deltaTime * 5;
        Vector3 currentPosition = Vector3.Lerp(startingPosition, targetPosition, lerpPosition);
        return currentPosition;
    }
}
