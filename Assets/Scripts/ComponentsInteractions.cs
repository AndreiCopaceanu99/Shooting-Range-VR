using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentsInteractions : MonoBehaviour
{
    [SerializeField] string componentName;

    [System.NonSerialized] public bool isInteracting = false;
    public Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Interact(Vector3 handlerPosition)
    {
        isInteracting = true;

        switch(componentName)
        {
            case "Slider":
                SliderInteraction slider = GetComponent<SliderInteraction>();
                slider.MoveSlider(handlerPosition, 200f);
                return;
            case "SafetyLock":
                transform.localRotation = Quaternion.Euler(
                    ComponentMovement.ComponentPosition(
                        transform.localRotation.eulerAngles, 
                        targetPosition, 
                        5f)
                    );
                return;
        }
    }
}
