using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentsInteractions : MonoBehaviour
{
    [SerializeField] string componentName;

    [System.NonSerialized] public bool isInteracting = false;

    public float speed;

    public GunManager gunManager;

    public void Interact(Vector3 handlerPosition, Vector3 localHandlerPosition, Vector3 handlerRotation)
    {
        isInteracting = true;

        switch(componentName)
        {
            case "Slider":
                SliderInteraction slider = GetComponent<SliderInteraction>();
                /*float distance = Vector3.Distance(
                        transform.localPosition,
                        localHandlerPosition
                        );*/
                slider.MoveSlider(
                    localHandlerPosition,
                    speed / 2
                    );
                return;
            case "SafetyLock":
                SafetyLock safetyLock = GetComponent<SafetyLock>();
                safetyLock.ChangeSafetyPosition();
                return;
            case "MagazineCatch":
                MagazineCatch magazineCatch = GetComponent<MagazineCatch>();
                magazineCatch.MagazineCatchPressed();
                return;
            case "Magazine":
                Magazine magazine = GetComponent<Magazine>();
                magazine.HandValues(handlerPosition, handlerRotation);
                return;
        }
    }
}
