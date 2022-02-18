using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineCatch : ComponentsInteractions
{
    bool move = false;

    Vector3 initialPosition;
    [SerializeField] Vector3 pressedPosition;

    public Magazine magazine;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInteracting && move)
        {
            MoveMagazineCatch(initialPosition, speed);
        }
        else
        {
            MoveMagazineCatch(pressedPosition, speed);
        }
    }

    public void MagazineCatchPressed()
    {
        if (!move)
        {
            move = true;
            if (magazine != null)
            {
                UnloadMagazine();
            }
        }
    }

    void MoveMagazineCatch(Vector3 targetPosition, float speed)
    {
        transform.localPosition = ComponentMovement.ComponentPosition(transform.localPosition, targetPosition, speed);

        if (transform.localPosition == targetPosition)
        {
            move = false;
        }
    }

    void UnloadMagazine()
    {
        Rigidbody rb = magazine.GetComponent<Rigidbody>();
        magazine.transform.parent = null;
        rb.isKinematic = false;
        rb.AddForce(-magazine.transform.up * 100);
        magazine = null;
    }
}
