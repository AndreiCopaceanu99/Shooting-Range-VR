using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : ComponentsInteractions
{
    public bool loaded;

    [SerializeField] Vector3 loadedPosition;
    [SerializeField] Vector3 loadedRotation;

    Rigidbody rb;

    bool pickedUp;

    Vector3 position;
    Vector3 rotation;

    MagazineCatch magazineCatch;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        magazineCatch = GameObject.FindObjectOfType<MagazineCatch>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!loaded && pickedUp)
        {
            PickUpGun();
        }
        
        if(magazineCatch.magazine != gameObject.GetComponent<Magazine>())
        {
            loaded = false;
        }
        else
        {
            LoadedTransform();
        }

        if(!isInteracting)
        {
            pickedUp = false;
            rb.useGravity = true;
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
        rb.useGravity = false;
        transform.position = PickUp.PickUpObject(transform.position, position);
        transform.rotation = Quaternion.Euler(rotation);
    }

    void LoadedTransform()
    {
            transform.localPosition = loadedPosition;
            transform.localEulerAngles = loadedRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Gun")
        {
            loaded = true;
            transform.parent = other.transform;
            rb.isKinematic = true;

            magazineCatch.magazine = gameObject.GetComponent<Magazine>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Gun")
        {
            loaded = false;
        }
    }
}
