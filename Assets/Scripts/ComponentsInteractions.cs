using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentsInteractions : MonoBehaviour
{
    [SerializeField] string componentName;

    [System.NonSerialized] public bool isInteracting = false;

    public float speed;

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
                slider.MoveSlider(
                    handlerPosition, 
                    Vector3.Distance(
                        new Vector3(0, 0, transform.localPosition.z),
                        new Vector3(0, 0, -handlerPosition.z) * speed
                        )
                    );
                Debug.Log(handlerPosition);
                return;
            case "SafetyLock":
                SafetyLock safetyLock = GetComponent<SafetyLock>();
                safetyLock.ChangeSafetyPosition();
                return;
        }
    }
}
