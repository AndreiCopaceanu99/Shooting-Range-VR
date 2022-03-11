using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandControllerInputs : MonoBehaviour
{
    MeshFilter mesh;

    Rigidbody rb;

    [SerializeField] LayerMask interactableObjectLayer;
    [SerializeField] float rayMaxDistance;

    GameObject interactableObject;

    [SerializeField] GameObject magazinePrefab;
    [SerializeField] Vector3 magazineArea;
    [SerializeField] float magazineAreaDistance;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>();
    }

    private void Update()
    {
        checkObjects();
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
        HandedDesiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Left | UnityEngine.XR.InputDeviceCharacteristics.Controller;
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
                    mesh.mesh = null;
                    //transform.GetChild(0).gameObject.SetActive(false);
                    if (interactableObject.tag == "Components")
                    {
                        ComponentsInteractions component = interactableObject.GetComponent<ComponentsInteractions>();
                        component.Interact(transform.position, transform.localPosition, transform.localRotation.eulerAngles);
                    }
                }
            }
            else
            {
                if (interactableObject != null && interactableObject.GetComponent<ComponentsInteractions>() != null && interactableObject.tag == "Components")
                {
                    interactableObject.GetComponent<ComponentsInteractions>().isInteracting = triggerValue;
                    checkObjects();
                    interactableObject = null;
                }
            }

            bool gripValue;
            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out gripValue) && gripValue)
            {
                if (interactableObject != null)
                {
                    mesh.mesh = null;
                    //transform.GetChild(0).gameObject.SetActive(false);
                    if (interactableObject.tag == "Slider" || interactableObject.tag == "Magazine")
                    {
                        ComponentsInteractions component = interactableObject.GetComponent<ComponentsInteractions>();
                        component.Interact(transform.position, transform.localPosition, transform.localRotation.eulerAngles);
                    }

                }
                else
                {
                    if (Vector3.Distance(transform.localPosition, magazineArea) <= magazineAreaDistance)
                    {
                        interactableObject = Instantiate(magazinePrefab, transform.position, Quaternion.identity);
                    }
                }

            }
            else
            {
                if (interactableObject != null && interactableObject.GetComponent<ComponentsInteractions>() != null && (interactableObject.tag == "Slider" || interactableObject.tag == "Magazine"))
                {
                    interactableObject.GetComponent<ComponentsInteractions>().isInteracting = gripValue;
                    checkObjects();
                    interactableObject = null;
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
            if (interactableObject == null)
            {
                interactableObject = hit.collider.transform.gameObject;
            }

            if (interactableObject != hit.collider.transform.gameObject)
            {
                changeColour(Color.white);
                interactableObject = hit.collider.transform.gameObject;
            }

            //Debug.Log(interactableObject.name + " " + interactableObject.layer.ToString());

            changeColour(Color.yellow);
        }
        else if (interactableObject != null)
        {
            changeColour(Color.white);
        }
    }

    void changeColour(Color color)
    {
        ChangeColourInteractableObjects hitObject = interactableObject.GetComponent<ChangeColourInteractableObjects>();
        hitObject.changeColour(color);
    }
}
