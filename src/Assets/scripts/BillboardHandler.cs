using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardHandler : MonoBehaviour {

    private Camera lookCamera;

    private void Start()
    {
        lookCamera = Camera.main;        
    }

    void Update()
    {
        Vector3 lookVector = lookCamera.transform.position - transform.position;
        if (lookVector.magnitude > 5)
        {
            lookVector.x = lookVector.z = 0.0f;
            lookVector.y = 90;
            transform.LookAt(lookCamera.transform.position - lookVector);
        }
    }
}
