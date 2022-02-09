using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInteraction : MonoBehaviour
{
    bool pickedUp;

    Vector3 position;
    Vector3 rotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pickedUp)
        {
            PickUpGun();
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
        //transform.rotation = Quaternion.Euler(PickUp.PickUpObject(transform.rotation.eulerAngles, rotation));
        transform.rotation = Quaternion.Euler(rotation + new Vector3(180f, -180f, 180f));
    }
}
