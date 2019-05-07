using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericCustomGravityHandler : MonoBehaviour {

    private Rigidbody rigidbody;
    public float gravity = 10.0f;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.freezeRotation = true;
        rigidbody.useGravity = false;
    }

}
