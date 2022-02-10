using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColourInteractableObjects : MonoBehaviour
{
    List<MeshRenderer> kidsMaterial = new List<MeshRenderer>();
    MeshRenderer selfMaterial;

    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<MeshRenderer>())
        {
            selfMaterial = GetComponent<MeshRenderer>();
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            kidsMaterial.Add(transform.GetChild(i).transform.gameObject.GetComponent<MeshRenderer>());
        }
    }

    public void changeColour(Color materialColor)
    {
        foreach (MeshRenderer gunComponent in kidsMaterial)
        {
            gunComponent.material.color = materialColor;
        }
        if (selfMaterial != null)
        {
            selfMaterial.material.color = materialColor;
        }
    }
}
