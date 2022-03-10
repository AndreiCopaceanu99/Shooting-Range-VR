using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpact : MonoBehaviour
{
    [SerializeField] GameObject impactHoleVFX;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Target")
        {
            //Debug.Log(transform.position);
            //Instantiate(impactHoleVFX, transform.position - new Vector3(0f, 0f, 0.2f), Quaternion.identity);

            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, contact.normal);
            Vector3 pos = contact.point;

            Instantiate(impactHoleVFX, pos, rot);
            Destroy(gameObject);
        }
    }
}
