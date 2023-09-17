using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractHandler : MonoBehaviour
{
    public float maxRayDistance = 1000;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool isHit = Physics.Raycast(ray,out hit, maxRayDistance);
            if (isHit)
            {
                GameObject hitObject = hit.collider.gameObject;
                Interactable interactable = hitObject.GetComponent<Interactable>();
                interactable?.Hit();
            }
        }
    }
}

public interface Interactable
{
    void Hit();
}