using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInteraction : MonoBehaviour
{
    bool pickedUp;

    Vector3 position;
    Vector3 rotation;

    Rigidbody rb;

    public bool isInteracting;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pickedUp)
        {
            PickUpGun();
            rb.isKinematic = true;
        }

        if (!isInteracting)
        {
            pickedUp = false;
            rb.isKinematic = false;
        }
    }

    public void HandValues(Vector3 handPosition, Vector3 handRotation)
    {
        pickedUp = true;
        position = handPosition;
        rotation = handRotation;
    }

    void PickUpGun()
    {
        transform.position = PickUp.PickUpObject(transform.position, position);
        transform.rotation = Quaternion.Euler(rotation);
    }
}
