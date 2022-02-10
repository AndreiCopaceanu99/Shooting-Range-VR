using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandControllerInputs : MonoBehaviour
{
    MeshFilter mesh;

    Rigidbody rb;

    [SerializeField] LayerMask interactableObjectLayer;
    [SerializeField] float rayMaxDistance;

    bool armed;
    GameObject interactableObject;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>();
    }

    private void Update()
    {
        if (armed)
        {
            handleGun();
        }
        else
        {
            checkObjects();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetHandedControllerComponents();
    }

    void GetHandedControllerComponents()
    {
        List<UnityEngine.XR.InputDevice> HandedControllers;
        UnityEngine.XR.InputDeviceCharacteristics HandedDesiredCharacteristics;
        HandedControllers = new List<UnityEngine.XR.InputDevice>();
        HandedDesiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
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
                if (interactableObject != null)
                {
                    armed = true;
                    mesh.mesh = null;
                    transform.GetChild(0).gameObject.SetActive(false);
                    changeColour(Color.white);
                }
                else
                {

                }
            }
        }
    }

    void checkObjects()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayMaxDistance, interactableObjectLayer))
        {
            if(interactableObject == null)
            {
                interactableObject = hit.transform.gameObject;
            }

            if (interactableObject != hit.transform.gameObject)
            {
                changeColour(Color.white);
                interactableObject = hit.transform.gameObject;
            }

            changeColour(Color.yellow);
        }
        else if(interactableObject != null)
        {
            changeColour(Color.white);
        }
    }

    void changeColour(Color color)
    {
        ChangeColourInteractableObjects hitObject = interactableObject.GetComponent<ChangeColourInteractableObjects>();
        hitObject.changeColour(color);
    }

    void handleGun()
    {
        GunInteraction hitObject = interactableObject.GetComponent<GunInteraction>();
        hitObject.HandValues(transform.position, transform.localRotation.eulerAngles);
    }
}
