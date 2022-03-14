using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderInteraction : ComponentsInteractions
{
    Vector3 startingPosition;
    Vector3 targetPosition;

    Vector3 initialHandPosition;

    bool grabbed;

    bool bulletRemoved;

    [SerializeField] float maxSlider;
    [SerializeField] float minSlider;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] Vector3 caseForce;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.localPosition;
        gunManager = FindObjectOfType<GunManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInteracting)
        {
            initialHandPosition = startingPosition;
            grabbed = false;
            MoveSlider(startingPosition, speed);
        }
    }

    public void MoveSlider(Vector3 handlerPosition, float speed)
    {
       // targetPosition = handlerPosition;

        if(!grabbed && isInteracting)
        {
            initialHandPosition = handlerPosition;
            grabbed = true;
        }

        float distance = -Vector3.Distance(initialHandPosition, handlerPosition);

        if (isInteracting)
        {
            targetPosition = new Vector3(
                            transform.localPosition.x,
                            transform.localPosition.y,
                            distance
                            );

        }
        else
        {
            targetPosition = startingPosition;
        }
        
        //Debug.Log(targetPosition.z);

        targetPosition.z = Mathf.Clamp(targetPosition.z, minSlider, maxSlider);

        if(targetPosition.z == minSlider)
        {
            if(!gunManager.isClocked)
            {
                gunManager.isClocked = true;
            }
            else
            {
                if (!bulletRemoved)
                {
                    ReleaseBullet();
                }
            }
            bulletRemoved = true;
        }

        if(targetPosition.z == maxSlider)
        {
            bulletRemoved = false;
        }
        

        transform.localPosition = ComponentMovement.ComponentPosition(
                    transform.localPosition,
                    targetPosition,
                    speed);

        //Debug.Log(transform.localPosition.z);
    }

    void ReleaseBullet()
    {
        Magazine magazine = FindObjectOfType<MagazineCatch>().magazine;
        magazine.bulletsNumber--;

        Rigidbody caseRB = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity).GetComponent<Rigidbody>();
        caseRB.AddRelativeForce(transform.right * caseForce.x + transform.up * caseForce.y);
    }
}
