using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputs : MonoBehaviour
{
    [SerializeField] bool rightHand;

    MeshFilter mesh;

    Rigidbody rb;

    [SerializeField] LayerMask interactableObjects;
    [SerializeField] float rayMaxDistance;

    bool armed;
    GameObject gun;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>();
    }

    private void Update()
    {
        if(armed)
        {
            handleGun();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetHandedControllerComponents(rightHand);
    }

    void GetHandedControllerComponents(bool hand)
    {
        List<UnityEngine.XR.InputDevice> HandedControllers;
        UnityEngine.XR.InputDeviceCharacteristics HandedDesiredCharacteristics;
        if (hand)
        {
            HandedControllers = new List<UnityEngine.XR.InputDevice>();
            HandedDesiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        }
        else
        {
            HandedControllers = new List<UnityEngine.XR.InputDevice>();
            HandedDesiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Left | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        }
        Controller(HandedControllers, HandedDesiredCharacteristics);
    }

    void Controller(List<UnityEngine.XR.InputDevice> controller, UnityEngine.XR.InputDeviceCharacteristics characteristics)
    {
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(characteristics, controller);

        foreach (var device in controller)
        {
            Vector3 position;
            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.devicePosition, out position))
            {
                // NOTE: needs to be local position (i.e. position relative to rig)
                transform.localPosition = position;
            }

            Quaternion orientation;
            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceRotation, out orientation))
            {
                // NOTE: needs to be local rotation (i.e. rotation relative to rig)
                transform.localRotation = orientation;
            }

            bool triggerValue;
            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                if (!armed)
                {
                    RaycastHit hit;
                    // Does the ray intersect any objects excluding the player layer
                    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayMaxDistance, interactableObjects))
                    {
                        if (hit.transform.gameObject.tag == "Gun")
                        {
                            gun = hit.transform.gameObject;
                            armed = true;
                            mesh.mesh = null;
                            transform.GetChild(0).gameObject.SetActive(false);
                        }
                    }
                }
                else
                {

                }
            }
        }
    }

    void handleGun()
    {
        GunInteraction hitObject = gun.GetComponent<GunInteraction>();
        hitObject.HandValues(transform.position, transform.localRotation.eulerAngles);
    }
}
