using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadMovementHandler : MonoBehaviour {

    public Transform bodyTransform;
    public float mouseSensibility = 5.0f;

    void Update()
    {
        bodyTransform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * mouseSensibility, 0), Space.World);
        transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * mouseSensibility, 0, 0));
    }

}
