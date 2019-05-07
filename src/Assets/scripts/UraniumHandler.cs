using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UraniumHandler : MonoBehaviour {

    public bool isGrounded;
    private Rigidbody rigidbody;
    private float gravity;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        gravity = GetComponent<GenericCustomGravityHandler>().gravity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody.AddForce(new Vector3(0, -gravity * rigidbody.mass, 0));
        isGrounded = false;
	}

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            isGrounded = true;
        }
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
    

}
